Feature: ChequeCreate
	Checking the sites functionality

Scenario: Main Page Has Name Field
	When I Navigate To "Index"
	Then "Name" field should be available

Scenario: Main Page Has Amount Field
	When I Navigate To "Index"
	Then "Amount" field should be available

Scenario: Main Page Has Date Field
	When I Navigate To "Index"
	Then "Date" field should be available

Scenario: Validation Fields Appear
	Given I am on "Index" page
	When I Press "Create"
	Then "Name" Required Message Appears
	Then "Amount" Required Message Appears
	Then "Date" Required Message Appears	

Scenario: Check Amount Less Than 0
	Given I am on "Index" page
	And I enter "Test User" in "Name"
	And I enter "30/06/2014" in "Date"
	And I enter " -1" in "Amount"	
	When I Press "Create"
	Then "Amount" Required Message Appears
	Then "Name" Required Message Does Not Appear
	Then "Date" Required Message Does Not Appear

Scenario: Goes to Create page when successful 
	Given I am on "Index" page
	And I enter "Test User" in "Name"
	And I enter "30/06/2014" in "Date"
	And I enter "1" in "Amount"	
	When I Press "Create"
	Then I am on "Create" page

Scenario: Date Fields Is Correct On the Cheque 
	Given I am on "Index" page
	And I enter "Test User" in "Name"
	And I enter "30/06/2014" in "Date"
	And I enter "1" in "Amount"	
	When I Press "Create"
	Then Date Fields Are Correct

Scenario: Amount In Words 
	Given I am on "Index" page
	And I enter "Test User" in "Name"
	And I enter "30/06/2014" in "Date"
	And I enter "1" in "Amount"	
	When I Press "Create"
	Then Amount In Words is "One Dollar"

Scenario: Amount In Words Correct With Long Amount 
	Given I am on "Index" page
	And I enter "Test User" in "Name"
	And I enter "30/06/2014" in "Date"
	And I enter "323.64" in "Amount"	
	When I Press "Create"
	Then Amount In Words is "Three Hundred and Twenty Three Dollars and Sixty Four Cents"

Scenario: Name Is Correct On the Cheque
	Given I am on "Index" page
	And I enter "Test User" in "Name"
	And I enter "30/06/2014" in "Date"
	And I enter "323.64" in "Amount"	
	When I Press "Create"
	Then Name is "Test User"

Scenario: Amount Is Correct On the Cheque
	Given I am on "Index" page
	And I enter "Test User" in "Name"
	And I enter "30/06/2014" in "Date"
	And I enter "323.64" in "Amount"	
	When I Press "Create"
	Then Amount is "323.64"

Scenario: Amount Is Correct With Decimal Places On the Cheque
	Given I am on "Index" page
	And I enter "Test User" in "Name"
	And I enter "30/06/2014" in "Date"
	And I enter "10" in "Amount"	
	When I Press "Create"
	Then Amount is "10.00"
