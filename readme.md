PI ANALYSIS RECALCULATOR MANAGER USER’S GUIDE
Version 1.0
OVERVIEW
1.	Description
The PI Analysis Recalculator Manager is a tool to simplify scheduling selective analyses recalculations on large PI AF systems. It was built to help users quickly finding and selecting the analyses of interest for backfilling, recalculation and to enable and disabled analyses in bulk.
With this tool, users can retrieve the analyses list based for a target element (including or not the element child elements). Also, they can use combine the root element search with the utilization of analyses filter path to perform a more selective search. 
 The search returns a list of analyses based on the specific search criteria. You can sort the list by analyses path, name, status, template or scheduling information, allowing you to easily select the analyses for either backfilling or recalculation.
This tool is not intended in any way to replace the analyses the Management plugin of PI System Explorer. You will still need the Management plugin to check the analyses backfilling status. 
Scenarios where this tool would be helpful:
	•	You have to select a subset of analyses from an extensive list of analyses, where doing manually in PI System Explorer would be very time consuming
	•	You need to perform recalculation of analyses which outputs are stored in an older version of PI Data Archive (prior to 2016 R2), where the explicit deletion of tag values would be required.
2.	Pre-requisites
The application requires the following components:
	•	PI AF Client 2016 R2
	•	PI Analysis Service 2016 R2
	•	PI Server 2010 or greater
INSTRUCTIONS
1.	Searching the analyses of interest
You can search the analyses of all elements in a PI AF database, or you can retrieve only the analyses from a specific root element (including or not its child elements). Also, you can retrieve the analyses based on some filter criteria.
Simply follow the steps below:
	•	Click on Select Database button, and pick the PI AF database of interest.  
	•	If you are interested to perform the recalculation for a specific PI AF element, click on … button and select the PI AF element of interest. 
	•	To include the child elements on the search, check the option Include Child Elements. Click the Search button to see the analysis associated with the target element and their child elements.
		o	you can leave the Target Element box empty and check the box Include Child Elements if you want to consider all analyses in the search.
		o	To abort the search, click the Abort button.
	•	You can type the filter for the analysis path, using standard PI wildcards characters:
		o	* to mask multiple characters
		o	? to mask a single character 
	•	To sort a column (ascending order), click on the corresponding column header.
	•	Use the buttons below the grid to select / deselect the analysis, and to change the analyses status when necessary.
	•	If you desire to limit the amount of results returned by the search, set the Max. Search Results field accordingly.

2.	Analyses Backfilling
The backfilling will simply fill gaps in the PI Data Archive. It assumes there is no data for the backfill interval. Existing data is not going to be replaced in this scenario.
To execute the backfill operation, follow the steps below:
	•	After selecting the analyses of interest, enter the start and end times for the backfilling/recalculation period (using standard PI time format), and select the option Backfill (fill gaps only) 
	•	Click on Run Operation to execute the operation. This operation cannot be aborted.

PI Analysis service will queue analyses for backfilling and no further action will be required. Check the backfilling status using the Analyses Management plugin of PI System Explorer.

3.	Analyses Recalculation
There are 2 modes of recalculation available: 
	•	one for PI Data Archive 2016 R2 and newer
	•	another one for older versions of PI Data Archive

3.1.	Recalculation scenario 1: PI Data Archive version is 2016 R2 or newer

If the version of the PI Data Archive used as analyses output repository is 2016 R2 or newer, follow the steps below:
	•	Select the option Recalculate and check the box PI Data Archive 2016 R2 or newer.
	•	The program will prompt you to continue or to cancel the operation, since data will be deleted. 
		o	Click Yes, to launch the recalculation (i.e., PI Analysis service will manage the data deletion from the PI Archive, and you will not be able to abort this operation), or 
		o	Click No if you don’t want to delete data in the PI Data Archive. 

In this case PI Analyses service will queue analyses for recalculation and no further action will be required. Check the backfilling status using the Analyses Management plugin of PI System Explorer.

3.2.	Recalculation scenario 2: older PI Data Archive version

If the option selected is Recalculate and the option PI Data Archive is 2016 R2 or newer is NOT set, this tool will be responsible for deleting the output values prior to analyses recalculation, since PI Analyses will not be able to delete the pre-existing data in the archive for you. 
Please follow the steps below:
	•	Select the option Recalculate and keep the box PI Data Archive 2016 R2 or newer unchecked.
	•	The program will prompt you to continue or to cancel the operation, since it requires explicit history data deletion.
		o	Choose Yes, to have the analyses output history deleted
		o	 Choose No if you do not want to delete analyses output history (in this case the operation will be canceled). 
 	•	After the values are deleted, you will be prompted to proceed with the operation. The next step depends on different scenarios:
		o	PI Buffer Subsystem is running in your computer and properly configured
			#	Click Yes after making sure that the delete events have already been flushed to the PI Data Archive (standalone of collective), otherwise the PI Analysis service will try to backfill the calculations to the time interval that still contains some undeleted data.
		o	PI Data Archive used as analyses output repository is in High Availability (HA), and the PI Buffer Subsystem is disabled in your computer:
			#	Click Cancel, switch to the next member of the PI Collective, and re-run the same operation, until the analysis output values in all collective members are deleted. 
			#	To switch to another member of the PI HA collective, click on the PI Data Archives to open the PI Data Archives connection window, select the PI Data Archive collective name used as the analyses output repository and switch to the desired collective member
			#	After deleting the values from all members Click Yes to proceed.
		o	Stand-alone PI Data Archive used as analyses output repository and PI Buffer Subsystem not running
			#	Simply click Yes to proceed.

 
After clicking Yes, PI Analyses service will queue analyses for backfilling and no further action will be required. Check the backfilling status using the Analysis Management plugin of PI System Explorer.
4.	Analysis Status Setting
The status of the selected analyses can be changed to either Enabled or Disabled. Any selected analysis can be disabled, but only the analysis in the Disabled status can be enabled.

3.3.	Enabling analyses in bulk
In order to enabled the analyses of interest:
•	Select the analysis of interest from the analyses list (by checking the corresponding analysis checkboxes)
•	Click the Enable button. 

You will be informed if any of the selected analyses cannot be enabled. In this case, you will see an informational message like this:
 
Even if the analysis status may have been changed to Enabled, it may go to an error state if the PI Analysis service detects a problem in the analysis. In order to check the current analysis state:
•	Click the Refresh Status button. 

3.4.	Disabling analyses in bulk
In order to disable the analyses of interest:
	•	Select the analysis of interest from the analyses list (by checking the corresponding analysis checkboxes)
	•	Click the Disable button. 

LICENSING
Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License for the specific language governing permissions and limitations under the License.      
Third-party code
Some functionality provided in this software leverages the code provided by Rick Davin, published in March 22, 2015 in the following tutorial: “A faster way to get PIPoints from a large list of AFAttributes”. 

REVISION HISTORY
Version	Date	Author	Notes
1.0	20-Jan-2017	Fabiano Batista	Initial version



