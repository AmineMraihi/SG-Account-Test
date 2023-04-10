Feature: Bank Account Transactions
	As a user
    I want to get the top three spending categories for my bank account, excluding certain categories
    So that I can better manage my finances

@mytag
Scenario: Get top three spending categories
	Given the following bank account information:
        | Date       | Montant   | Devise   | Catégorie   |
        | 06/10/2022 | -504.61   | EUR      | Loisir      |
        | 15/10/2022 | -408.61   | JPY      | Transport   |
        | 16/08/2022 | -103.46   | USD      | Loisir      |
        | 02/11/2022 | -271.49   | JPY      | Santé       |
        | 17/07/2022 | -44.17    | USD      | Alimentation|

	When I get the top three spending categories, certain categories
	Then the result should be:
        | Catégorie    |
        | Loisir      |
        | Transport   |
        | Santé       |