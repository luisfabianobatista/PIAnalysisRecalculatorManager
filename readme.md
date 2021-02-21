# PI ANALYSIS RECALCULATOR MANAGER

## OVERVIEW

The PI Analysis Recalculator Manager is a tool to simplify scheduling selective analyses recalculations on large PI AF systems. It was built to help users quickly finding and selecting the analyses of interest for backfilling, recalculation and to enable and disabled analyses in bulk.

With this tool, users can retrieve the analyses list based for a target element (including or not the element child elements). Also, they can use combine the root element search with the utilization of analyses filter path to perform a more selective search. 
The search returns a list of analyses based on the specific search criteria. You can sort the list by analyses path, name, status, template or scheduling information, allowing you to easily select the analyses for either backfilling or recalculation. Once the list of analysis is populated, the user can further refine the selection of analyses by applying table filters that can be set on each individual column.

This tool does not implement all functionalities provided by the *Management* plugin of PI System Explorer. **You will still need the Management plugin to check the analyses backfilling status.**

Scenarios where this tool would be helpful:

- You have to select a subset of analyses from an extensive list of analyses, where doing manually in PI System Explorer would be very time consuming;
- You need to perform recalculation of analyses which outputs are stored in an older version of PI Data Archive (prior to 2016 R2), where the explicit deletion of tag values would be required.
  
### Pre-requisites
  
  The application requires the following components:
- PI AF Client 2016 R2 or later version;
- PI Analysis Service 2016 R2 of later versrion;
- PI Server 2010 or later version;
- Microsoft .Net Framwork 4.8 or later version;
- [AdvancedDataGridView package]()https://www.nuget.org/packages/DG.AdvancedDataGridView

## LICENSING

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.  

### Third-party code

Some functionality provided in this software leverages the code provided by Rick Davin, published in March 22, 2015 in the following tutorial: “A faster way to get PIPoints from a large list of AFAttributes”.

The 3rd-party package **AdvandedDataGridView** is subject to the **Microsoft Public License (Ms-PL)**.

## REVISION HISTORY

| Version | Date | Author | Notes |
| --- | --- | --- | --- |
| 1.0 | 20-Jan-2017 | Fabiano Batista | Initial release |
| 1.1 | 26-Jan-2021 | Fabiano Batista | Bug fixes |
| 1.2 | 21-Feb-2021 | Fabiano Batista | Added advanced filtering capabilites |