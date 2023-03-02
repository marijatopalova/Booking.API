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
