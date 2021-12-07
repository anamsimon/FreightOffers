# Freight Offer - Requesting multiple API
##### Requirement:
The concept is to request several companies' API for offers and select the best deal.
##### Conditions:
* No UI expected.
* No SQL required.
* Must be unit-tested.
##### Process Input:
* one set of data {{source address}, {destination address}, [{carton dimensions}]}
* Multiple API using the same data with different signatures
##### Process Output:
* All API respond with the same data in different formats
* Process must query, then select the lowest offer and return it in the least amount of time
##### Sample APIs 
Each with its own url and credentials
###### API1 (JSON)
    Input {contact address, warehouse address, package dimensions:[]}
    Output {total}
###### API2 (JSON)
    Input {consignee, consignor, cartons:[]}
    Output {amount}
###### API3 (XML)
    Input <xml><source/><destination/><packages><package/></packages></xml>
    Output <xml><quote/></xml>

##### Implementation:
There are two components in this project. 
- The main api ( here FreightOffersApi) receives the source address, destination address and package dimensions from request. It then makes background api calls to three external systems (here FreightOffersExternalServiceAPI) API
- The external apis receive request from the main api and respond the offer amount
###### Integration Diagram
![Integration Diagram](https://github.com/anamsimon/FreightOffers/blob/main/Integration%20Diagram.png?raw=true)
##### Tech
* ASP .Net Core 5.0
* Visual Studio 2022
* NUnit
##### How to Run 
###### Run the APIs from Visual Studio
*Main API - Run the project FreightOffers.Api using IISExpress at port 44371
*Other Companies API - Run the project FreightOffers.ExternalService.Api using IISExpress at port 44356
###### Consume the main API
Use following curl to consume
```sh
curl --location --request POST 'https://localhost:44371/api/Freight/BestOffer' \
--header 'Content-Type: application/json' \
--data-raw '{
    "sourceAddress": {
        "line1": "line1",
        "line2": "line2",
        "city": "city",
        "province": "province",
        "postcode": "postcode",
        "country": "country"
    },
    "destinationAddress": {
        "line1": "line1",
        "line2": "line2",
        "city": "city",
        "province": "province",
        "postcode": "postcode",
        "country": "country"
    },
    "packages": [
        {
            "height": 10.0,
            "width": 10.0,
            "depth": 10.0
        },
        {
            "height": 10.0,
            "width": 10.0,
            "depth": 10.0
        }
    ]
}'
```
###### The Response
After receiving the request the main API will call other companies api in parallel and from their response it will pick lowest offer and return the amount as decimal as response.
Success Response 
```sh
{
    "amount": 50
}
```
Failure Response
When input request is object is not valid
```sh
400 Bad Request
request is incorrect
```
When all external api fails
```sh
500 Internal Server Error
```
