The intention of this kind of booking system is to allow the users to Search options (example: hotel and flight) for a certain period of time (example: 10 to 15 of June) for a given destination (example: Barcelona). As a result of the search, the user receives multiple options from the system to choose from. When the user decides on a given option, make a request to Book it. Because the booking process usually takes longer to complete, the user will be able to know when the booking is completed and Check the Status of the booking (example: Pending, Success or Fail).
These are the necessary endpoints to make a booking as explained above.
The booking flow will contain 3 methods:
1. Search
2. Book
3. CheckStatus

Search endpoint:

The API supports 3 types of searches:
- HotelOnly - If “DepartureAirport” is not provided in the response FlightCode is empty.
- HotelAndFlight - If the search request contains “DepartureAirport” then the response is a combination of flights and hotels.
- LastMinuteHotels - If “FromDate” is in the next 45 days in the response FlightCode is empty (similar to hotel only). 

Book endpoint:

Based on request data, the following object is generated.
- BookingCode - random code (6 chars [0-9a-zA-Z])
- SleepTime - random number between 30-60
- BookingTime - (DateTime.Now)

CheckStatus endpoint:

The whole booking process can't be processed immediately, it might take some time, so we don't want the customer to wait. 
So, the booking will be completed after sleepTime has elapsed (in seconds). 
In the meantime, “Pending” message is returned from this endpoint.

Ex: for a booking, if we have sleepTime = 45 and BookingTime = '8/16/2022 1:45:43', the booking is completed after
'8/16/2022 1:46:28'.

For HotelOnly and HotelAndFlight the return message is Success, and for LastMinuteHotels the return message is Failed after the sleep time elapsed.

***************************************************************************************************************************

How to test Booking API App locally (with Postman)

***************************************************************************************************************************


1.	Header Authorization (for all endpoints)

KEY: ApiKey

Value: 1f5d5dFG23FREeh51weS56 

***************************************************************************************************************************

2.	Search endpoint:

https://localhost:7172/api/search

Body:

{

  "destination": "SKP",
  
  "departureAirport": "MLH",
  
  "fromDate": "2022-11-09T14:46:53.715Z",
  
  "toDate": "2022-11-09T14:46:53.715Z"
  
}


The response should be a combination of hotels and flights, similar to the following list:


{

    "options": [
    
        {
        
            "optionCode": "1234",
            
            "hotelCode": "1212",
            
            "flightCode": "1111",
            
            "arrivalAirport": "SKP",
            
            "price": 99
            
        },
        
        {
        
            "optionCode": "7890",
            
            "hotelCode": "1818",
            
            "flightCode": "7777",
            
            "arrivalAirport": "SKP",
            
            "price": 45
            
        }
        
    ]
    
}


Try to test this endpoint without providing data for departureAirport or adjust fromDate to be any day between today’s date and 45 days from today, the response should be a Hotel only search where the FlightCode field is empty. 

{
    "options": [
        {
            "optionCode": "1234",
            "hotelCode": "1212",
            "flightCode": "",
            "arrivalAirport": "SKP",
            "price": 99
        },
        {
            "optionCode": "7890",
            "hotelCode": "1818",
            "flightCode": "",
            "arrivalAirport": "SKP",
            "price": 45
        }
    ]
}


If data is not provided for destination, fromDate or toDate an exception is thrown - Required fields must be populated.


{

    "Success": false,
    
    "Message": "Required fields must be populated."
    
}


***************************************************************************************************************************

3.	Book endpoint

https://localhost:7172/api/book


Pick any of the options returned from the search response and populate the body based on that option’s data.

Body:

{

  "optionCode": "1234",
  
  "searchRequest": {
  
    "destination": "SKP",
    
    "departureAirport": "MLH",
    
    "fromDate": "2022-11-09T15:28:50.859Z",
    
    "toDate": "2022-11-09T15:28:50.859Z"
    
  }
  
}


This is how the response should look like:

{

    "bookingCode": "slmRrf",
    
    "bookingTime": "2023-03-02T19:03:29.8232537Z"
    
}



Try testing this endpoint with invalid optionCode (Ex. 9999). This should raise an exception with message - The option you are trying to book was not found.

{

  "optionCode": "9999",
  
  "searchRequest": {
  
    "destination": "SKP",
    
    "departureAirport": "MLH",
    
    "fromDate": "2022-11-09T15:28:50.859Z",
    
    "toDate": "2022-11-09T15:28:50.859Z"
    
  }
  
}


The response:

{

    "Success": false,
    
    "Message": "The option you are trying to book was not found."
    
}

***************************************************************************************************************************

4.	Check Status endpoint

https://localhost:7172/api/checkstatus


After the booking was successful copy the bookingCode from the booking response and paste it in the body for this request.



Body:

{

    "bookingCode": "iKgWCd"
    
}


The immediate response should look like this:


{

    "status": "Pending"
    
}



Wait sometime between 30 to 60 seconds and then send the request again to see the changes. The status should change to either Success or Fail, based on the search type.


Try testing this endpoint with invalid bookingCode (Ex. 1wh56p), the app should throw an exception with message  - Booking not found.


{

    "Success": false,
    
    "Message": "Booking not found"
    
}

