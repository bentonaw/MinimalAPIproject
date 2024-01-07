![ER-Diagram](https://github.com/bentonaw/MinimalAPIproject/assets/98620169/a10c5075-743a-426f-9a8f-e3eed7a93dc2)


![Class diagram drawio](https://github.com/bentonaw/MinimalAPIproject/assets/98620169/18f0891e-ba4e-4917-8f67-4f9ad85bad54)


## Person APIs

### Returns list of all persons
`app.MapGet("/persons", PersonHandler.ListPersons);`

### Return a list of all persons that include search query in either first or last name
`app.MapGet("/persons/{query}", PersonHandler.FilterPersons);`

### Return a view of a specific person, lists everything connected to person
`app.MapGet("/persons/{personId}", PersonHandler.ViewPerson);`

## Interest APIs

### Return a list of interests of a specific person
`app.MapGet("/persons/{personId}/interests", PersonInterestHandler.ListInterestsOfPerson);`

### Return a list of interests of a specific person that includes search query
`app.MapGet("/persons/{personId}/interests/{query}", PersonInterestHandler.FilterInterest);`

### Connects a person to new interest, if interest (by its title) already exists it connects person to said interest
`app.MapPost("/persons/{personId}/interests", PersonInterestHandler.ConnectPersonToInterest);`

## Links APIs

### Return a list of url links connected to person
`app.MapGet("/persons/{personId}/links", PersonInterestLinkHandler.ListLinkToInterestsOfPerson);`
### Return a list of url links connected to a person that includes search query
`app.MapGet("/persons/{personId}/links/{query}", PersonInterestLinkHandler.FilterInterestLinks);`

### Returns all links of an interest connected to a specific person
`app.MapPost("/persons/{personId}/{interestId}", PersonInterestLinkHandler.LinksOfInterest);`

### Connect new link to an interest of a specific user
`app.MapPost("/persons/{personId}/{interestId}", PersonInterestLinkHandler.AddLinkToInterestOfPerson);`
