# LeasingCompanyMS 

### Management system application for leasing company

#### Tech specifications:
* Target framework: .NET 8.0
* UI framework: WPF (Windows Presentation Foundation)
* Test framework: MSTest

#### Other application info:
* Output type: Windows Application
* Target OS: Windows
* Minimum OS version: 7.0

#### Functionalities:
* login (2 user types - user & admin)
* logout (back to login view)
* user actions:
  - browse cars
  - choose car
* admin actions:
  - manage cars
  - manage clients


#### Login credentials (Username/Password):
* admin/admin
* user/user

### How to use
Open LeasingCompanyMS.sln in Visual Studio. Project is ready to run.

### Login view


### User View 
Column on the left is used for navigating between pages and for logging out. The panel on the right displays currently selected page

### Available cars page
Table displays details all cars available for lease.
Combo boxes under the table allow selection of leasing options. Selecting all options and a desired car will calculate and display monthly lease installment. 
To lease selected vehicle press Lease button. A confirmation dialog will be shown, click Confirm to continue.

### My leasings
Combo box in "My Leasings" section contains leasings for the current user. Picking an option from the drop down list allows to preview the payment schedule in the table below.

### Manage cars
The table displays all cars, both available and leased. To add a ne car, click on the button below, fill in the details and confirm.

### Manage clients
Table displays all details of all clients, both admins and regular users. To add a new client, click on the button below, fill in the details and confirm.
