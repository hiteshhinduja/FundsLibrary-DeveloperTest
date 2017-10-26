Task wise implementation details

Followed TDD Approach for each task:
Added entities, interfaces, skeleton classes as applicable for the task
Added unit test case for all possible scenarios
Executed test cases for unimplemented code which resulted in failure
Implemented business logic for the task
Re-ran unit test cases and checked whether they passed

Task 1
1.1 Delivery Driver Analysis

Created AnalysisConfiguration entity for storing Permitted start and end times along with maximum speed allowed and rating to be given if average speed exceeds maximum speed
Created Result entity for storing rating, duration, start and end times for each period considered (documented and undocumented)
Implemented code to analyse data in DeliveryDriverAnalyser by considering documented and undocumented periods as per the configuration given.
Created Helper class RatingCalculator to calculate overall rating from the given result set
After successfully determining ratings and durations for periods to be considered, called the RatingCalculator with the list of Result entity to get the overall rating.


1.2 Formula One Driver Analysis

Implemented code to analyse data in FormulaOneAnalyser by considering documented and undocumented periods as per the configuration given.
After successfully determining ratings and durations for periods to be considered, called the RatingCalculator with the list of Result entity to get the overall rating.

1.3 Getaway Driver Analysis

Implemented code to analyse data in GetawayDriverAnalyser by considering documented and undocumented periods as per the configuration given.
After successfully determining ratings and durations for periods to be considered, called the RatingCalculator with the list of Result entity to get the overall rating.

1.4 Penalise Faulty Recording

Added a property PenaltyForFaultyRecording in AnalysisConfiguration entity to determine how much penalty is to be applied if undocumented periods exist
Added a property DriverRatingAfterPenalty in HistoryAnalysis entity to store the rating after penalty is applied (if not applied, it will be same as DriverRating)
Updated the code for all analysers to apply penalty after calculating overall rating if any undocumented periods exist.

Task 2 Better Analyser Lookup(Dependency Inversion)

Added a dictionary in AnalyserLookup class which contains registered analysers along with a delegate call to respective analyser constructor which takes AnalysisConfiguration as input and returns analyser instance with given configuration
AnalyserLookup refers this dictionary with the analyser type received to return appropriate analyser instance.

Task 3 Read Canned Data from file

Chose .csv as data file containing CannedData
Added unit test case for parsing csv data
Added unit test cases for each analyser where data is read from file
Created .csv file containing information for different periods
Added base directory path in app.config of tests and console. This path points to the directory where .csv data files will be available
Created a FileReader which accepts absolute path containing file name as input and returns file content as string
Created a CsvDataParser which accepts string data as input, parses the data and returns list of periods

Task 4 Improve the tests

Added few more negative scenario test cases as majority of the test cases were already in place.
Created SetUp method which will be called before each test case execution which contains common code required by each test.