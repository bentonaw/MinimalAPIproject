

# Person APIs (for 1st version)

### Returns list of all persons
`GET /persons`

### Return a list of all persons that include search query in either first or last name
_E.g: /persons/search?query=John_

`GET /persons/search`

### Return a view of a specific person, lists everything connected to the person
`GET /persons/{personId}`

## Interest APIs

### Return a list of interests of a specific person
`GET /persons/{personId}/interests`

### Return a list of interests of a specific person that includes search query
E.g: _/persons/1/interests/search?query=Tennis_

`GET /persons/{personId}/interests/search`

### Connects a person to a new interest, if interest (by its title) already exists, it connects the person to said interest
E.g: _{
    "title": "Sample Interest",
    "description": "Description of the interest",
}_

`POST /persons/{personId}/interests`

## Links APIs

### Return a list of URL links connected to a person
`GET /persons/{personId}/links`

### Returns all links of an interest connected to a specific person
`GET /persons/{personId}/interests/{interestId}/links`

_not working properly_
### Connect new link to an interest of a specific user
E.g: _{
    "linkToInterest": "samplelink"
}_

`POST /persons/{personId}/interests/{interestId}/links`



# Person APIs (for 2nd version)

### Returns list of all people
`GET /people`

### Return a list of all people that include search query in either first or last name
_E.g: /people/search?query=John_
`GET /people/search`

### Return a view of a specific person, lists everything connected to the person
`GET /people/{personId}`

### Add new person
_E.g: _{
    "firstName": "John",
     "lastName": "Doe"
 }_

`POST /people`

### Add new phonenumber to person
_E.g: _{
    "number": "123456789"
 }_

`POST /people/{personId}`

## Interest APIs

### Return a list of interests of a specific person
`GET /people/{personId}/interests`

### Return a list of interests of a specific person that includes search query
E.g: _/people/1/interests/search?query=Tennis_

`GET /people/{personId}/interests/search`

### Connects a person to a new interest, if interest (by its title) already exists, it connects the person to said interest
E.g: _{
    "title": "Sample Interest",
    "description": "Description of the interest",
}_

`POST /people/{personId}/interests`

## Links APIs

### Return a list of URL links connected to a person
`GET /people/{personId}/links`

### Returns all links of an interest connected to a specific person
`GET /people/{personId}/interests/{interestId}/links`

### Connect new link to an interest of a specific user
E.g: _{
    "linkToInterest": "samplelink"
}_

`POST /people/{personId}/interests/{interestId}/links`



![ER-Diagram](https://github.com/bentonaw/MinimalAPIproject/assets/98620169/a10c5075-743a-426f-9a8f-e3eed7a93dc2)




![Class diagram drawio](https://github.com/bentonaw/MinimalAPIproject/assets/98620169/18f0891e-ba4e-4917-8f67-4f9ad85bad54)


