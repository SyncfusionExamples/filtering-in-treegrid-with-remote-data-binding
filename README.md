# filtering-in-treegrid-with-remote-data-binding

This sample demonstrates how to implement flexible filtering functionality in a TreeGrid control that uses remote data binding. The sample shows how users can apply filters to tree-structured rows while keeping the UI synchronized with a remote data source, preserving hierarchical relationships and enabling responsive, server-backed queries.

**Overview**

The project provides a minimal, focused example of integrating client-side TreeGrid filtering with server-side data operations. It highlights common scenarios such as text and numeric filtering, how filter state is sent to the server, and how filtered results are returned and displayed while preserving parent-child relationships in the grid.

**Features**

- Demonstrates client-initiated filtering with remote data binding
- Preserves hierarchical tree structure while applying filters
- Illustrates sending filter criteria to the server and rendering filtered results

**Prerequisites**

- A compatible web application environment that supports the TreeGrid control and remote endpoints
- A server endpoint that accepts filter parameters and returns filtered hierarchical data

**Installation & Usage**

Clone the repository, open the sample in your development environment, and run the web application. Use the TreeGrid UI to enter filter criteria; the grid will forward parameters to the configured remote endpoint and update rows according to the server response.

**Configuration**

Adjust the sample's remote data endpoint and any server-side query logic to match your backend. Ensure the server returns hierarchical results compatible with the TreeGrid's expected data shape.

**Topics**

- treegrid
- filtering
- remote-data-binding

**Notes & Support**

This sample is intended as a concise reference to understand remote filtering patterns for hierarchical grids. Adapt and extend the example to match your application's data model and server API.


