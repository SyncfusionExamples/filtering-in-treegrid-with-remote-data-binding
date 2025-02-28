using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemoteSave.Models;
using Syncfusion.EJ2.Base;
using System.Collections;
using Syncfusion.EJ2.Linq;
using Syncfusion.EJ2.Navigations;


namespace RemoteSave.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public class ExtDataManagerRequest : DataManagerRequest
        {
            public string filter_mode { get; set; } = "Parent";

        }

        public ActionResult DataSource(ExtDataManagerRequest dm)
        {
            List<TreeGridItems> data = new List<TreeGridItems>();
            data = TreeGridItems.GetSelfData();
            DataOperations operation = new DataOperations();
            IEnumerable<TreeGridItems> DataSource = data;

            if (!(dm.Where != null && dm.Where.Count > 1))
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, "and");

            }

            if (dm.Search != null && dm.Search.Count > 0) // Searching
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0 && dm.Sorted[0].Name != null) // Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 1) //filtering
            {
                if (dm.filter_mode == "Parent")
                {
                    //Filter the current filter object
                    DataSource = operation.PerformFiltering(DataSource, dm.Where[1].predicates, "and");

                    var parentData = Enumerable.Empty<TreeGridItems>();
                    var parentPredicates = new List<WhereFilter>();
                    if (DataSource.Any() && DataSource.First().ParentId != null)
                    {

                        //generate predicate of the equivalent parent record of filtered child record
                        var parentDataPredicate = new WhereFilter() { Field = "TaskId", value = DataSource.ToList<TreeGridItems>().First().ParentId, Operator = "equal" };
                        parentPredicates.Add(parentDataPredicate);

                        parentData = operation.PerformFiltering(TreeGridItems.BusinessObjectCollection, parentPredicates, "and");
                    }

                    //If the filtered data does not contain parent records, create a filter query to filter the parent records of the filtered data.


                    // concate the parent and filtered records
                    DataSource = (DataSource.Cast<TreeGridItems>()).Concat((IEnumerable<TreeGridItems>)parentData);

                }

                else if (dm.filter_mode == "Child" || dm.filter_mode == "Both")
                {
                    //Filter the current filter object
                    DataSource = operation.PerformFiltering(DataSource, dm.Where[1].predicates, "and");  //filter the child records by passing the query

                    var parentPredicates = new List<WhereFilter>();
                    var parentData = Enumerable.Empty<TreeGridItems>();
                    var childData = Enumerable.Empty<TreeGridItems>();
                    //generate predicate of the equivalent parent record of filtered child record
                    var parentDataPredicate = new WhereFilter() { Field = "TaskId", value = DataSource.ToList<TreeGridItems>().First().ParentId, Operator = "equal" }; parentPredicates.Add(parentDataPredicate);

                    parentData = operation.PerformFiltering(TreeGridItems.BusinessObjectCollection, parentPredicates, "and");

                    childData = operation.PerformFiltering(parentData, dm.Where[1].predicates, "and");

                    if (parentData.ToList().Count() > 0 && dm.filter_mode == "Both")
                    {// concate the parent and filtered records
                        DataSource = (DataSource.Cast<TreeGridItems>()).Concat((IEnumerable<TreeGridItems>)parentData);
                    }
                    else if (parentData.ToList().Count() > 0 && dm.filter_mode == "Child")
                    {// concate the parent and child filtered records

                        DataSource = (DataSource.Cast<TreeGridItems>()).Concat((IEnumerable<TreeGridItems>)childData);
                    }
                    else if (parentData.ToList().Count() == 0)
                    {
                        var childparentPredicates = new List<WhereFilter>();
                        //generate predicate of the  parent record from filtered record
                        var childparentDataPredicate = new WhereFilter() { Field = "ParentId", value = null, Operator = "equal" };
                        childparentPredicates.Add(childparentDataPredicate);
                        var parent_Data = operation.PerformFiltering(DataSource, childparentPredicates, "and");
                        //Filter the all the child records
                        var child_Data = TreeGridItems.BusinessObjectCollection.ToList().Where(item => item.ParentId == parent_Data.ToList<TreeGridItems>().First().TaskId);
                        //Concate parnet and child records
                        DataSource = child_Data.Concat((IEnumerable<TreeGridItems>)parent_Data);
                    }
                }
                else if (dm.filter_mode == "None")
                {
                    //Filter the current filter object
                    DataSource = operation.PerformFiltering(DataSource, dm.Where[1].predicates, "and");  //filter the child records by passing the query

                }
            }

            int count = DataSource.ToList<TreeGridItems>().Count;

            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public class TreeGridItems
        {
            public TreeGridItems() { }
            public int TaskId { get; set; }



            public string TaskName { get; set; }
            public int Duration { get; set; }
            public int? ParentId { get; set; }
            public bool? isParent { get; set; }


            public static List<TreeGridItems> BusinessObjectCollection = new List<TreeGridItems>();



            public static List<TreeGridItems> GetSelfData()
            {
                if (BusinessObjectCollection.Count == 0)
                {
                    int numberOfParentTasks = 20; // Define how many parent tasks you want
                    int taskId = 1; // Starting TaskId

                    for (int i = 0; i < numberOfParentTasks; i++)
                    {
                        // Add a parent task
                        BusinessObjectCollection.Add(new TreeGridItems()
                        {
                            TaskId = taskId++,
                            TaskName = $"Parent Task {i + 1}",
                            Duration = 10,
                            ParentId = null,
                            isParent = true
                        });

                        int parentId = taskId - 1; // Current parent task ID

                        // Add 5 child tasks for each parent task
                        for (int j = 0; j < 5; j++)
                        {
                            BusinessObjectCollection.Add(new TreeGridItems()
                            {
                                TaskId = taskId++,
                                TaskName = $"Child Task {i + 1}-{j + 1}",
                                Duration = 4,
                                ParentId = parentId,
                                isParent = false
                            });
                        }
                    }
                }
                return BusinessObjectCollection;
            }
        }
    }

}
