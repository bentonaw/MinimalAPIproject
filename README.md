

## Person APIs

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
    "Title": "Sample Interest",
    "Description": "Description of the interest",
}_

`POST /persons/{personId}/interests`

## Links APIs

### Return a list of URL links connected to a person
`GET /persons/{personId}/links`

### Returns all links of an interest connected to a specific person
`POST /persons/{personId}/{interestId}`

### Connect new link to an interest of a specific user
`POST /persons/{personId}/{interestId}`


![ER-Diagram](https://github.com/bentonaw/MinimalAPIproject/assets/98620169/a10c5075-743a-426f-9a8f-e3eed7a93dc2)


![Class diagram drawio](https://github.com/bentonaw/MinimalAPIproject/assets/98620169/18f0891e-ba4e-4917-8f67-4f9ad85bad54)
