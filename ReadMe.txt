Task wise implementation details (TDD Approach)

Task 1
1.1 Delivery Driver Analysis

Created AnalysisConfiguration entity for storing Permitted start and end times along with maximum speed allowed
Created Result entity for storing rating, duration, start and end times for each period considered (documented and undocumented)
Added failing unit test cases and data for different possible scenarios
Implemented code to analyse data in DeliveryDriverAnalyser by considering documented and undocumented periods as per the configuration given.
Created Helper class RatingCalculator to calculate overall rating from the given result set
After successfully determining ratings and durations for periods to be considered, called the RatingCalculator with the list of Result entity to get the overall rating.
Re-ran unit test cases and checked whether they passed

1.2 Formula One Driver Analysis

Added failing unit test cases and data for different possible scenarios
Implemented code to analyse data in FormulaOneAnalyser by considering documented and undocumented periods as per the configuration given.
After successfully determining ratings and durations for periods to be considered, called the RatingCalculator with the list of Result entity to get the overall rating.
Re-ran unit test cases and checked whether they passed

1.3 Getaway Driver Analysis

Added initially failing unit test cases and data for different possible scenarios
Implemented code to analyse data in GetawayDriverAnalyser by considering documented and undocumented periods as per the configuration given.
After successfully determining ratings and durations for periods to be considered, called the RatingCalculator with the list of Result entity to get the overall rating.
Re-ran unit test cases and checked whether they passed

1.4 Penalise Faulty Recording

Added failing unit test cases for rating with and without penalty for all 3 analyser tests
Added a property PenaltyForFaultyRecording in AnalysisConfiguration entity to determine how much penalty is to be applied if undocumented periods exist
Added a property DriverRatingAfterPenalty in HistoryAnalysis entity to store the rating after penalty is applied (if not applied, it will be same as DriverRating)
Updated the code for all analysers to apply penalty after calculating overall rating if any undocumented periods exist.
Re-ran unit test cases and checked whether they passed

Task 2 Better Analyser Lookup(Dependency Inversion)

Added failing unit test cases for Analyser lookup GetAnalyser for each analyser type and an invalid analyser type as negative test case
Created analyser factory interface and factory classes for each analyser with CreateAnalyser method that takes AnalysisConfiguration as input and returns analyser instance with that configuration
Added a dictionary in AnalyserLookup class which contains registered analysers along with a delegate call to respective analyser factories
AnalyserLookup refers this dictionary with the analyser type received to return appropriate analyser instance which is in turn provided by factory.
Re-ran unit test cases and checked whether they passed

Task 3 Read Canned Data from file

Added failing unit test cases for data reader for different possible scenarios
Added failing unit test cases for each analyser where data is read from file
Created .csv file containing information for different periods
Added base directory path in app.config of tests and console. This path points to the directory where .csv data files will be available
Created a CannedDataReader which accepts file name as input, parses the file and returns list of periods
Re-ran unit test cases and checked whether they passed

Task 4 Improve the tests

Added few more negative scenario test cases as majority of the test cases were already in place.
Created SetUp method which will be called before each test case execution which contains common code required by each test.