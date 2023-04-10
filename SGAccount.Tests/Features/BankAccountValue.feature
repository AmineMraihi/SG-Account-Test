Feature: Bank Account Value at given date
	As a user
    I want to get my bank account value at a given date
    So that I can have a better idea how I spent my money

@mytag
Scenario: Get my bank account value
	Given the following bank account information:
        | Date       | Montant   | Devise   | Catégorie    |
        | 20/02/2023 | -504.61   | EUR      | Loisir       |
        | 20/02/2023 | -408.61   | USD      | Transport    |
        | 16/08/2022 | -103.46   | JPY      | Loisir       |
        
	When I check my bank account at a given date '20/02/2023'
	Then I should have the result should be '9395.05145'