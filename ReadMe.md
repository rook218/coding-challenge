# Completed Coding Challenge

### Task 1
Create a new type, ReportingStructure, that has two properties: employee and numberOfReports and create an endpoint to return it from the data layer on the fly.

Get request to http://localhost:8080/api/reportingstructure/{topLevelEmployeeId}

Will return a nested object like:

```json
{
    "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
    "employee": {
        "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
        "firstName": "John",
        "lastName": "Lennon",
        "position": "Development Manager",
        "department": "Engineering",
        "directReports": [
            {
                "employeeId": "b7839309-3348-463b-a7e3-5de1c168beb3",
                "firstName": "Paul",
                "lastName": "McCartney",
                "position": "Developer I",
                "department": "Engineering",
                "directReports": []
            },
            {
                "employeeId": "03aa1462-ffa9-4978-901b-7c001562cf6f",
                "firstName": "Ringo",
                "lastName": "Starr",
                "position": "Developer V",
                "department": "Engineering",
                "directReports": [
                    {
                        "employeeId": "62c1084e-6e34-4630-93fd-9153afb65309",
                        "firstName": "Pete",
                        "lastName": "Best",
                        "position": "Developer II",
                        "department": "Engineering",
                        "directReports": []
                    },
                    {
                        "employeeId": "c0c2293d-16bd-4603-8e08-638a9d18b22c",
                        "firstName": "George",
                        "lastName": "Harrison",
                        "position": "Developer III",
                        "department": "Engineering",
                        "directReports": []
                    }
                ]
            }
        ]
    },
    "numberOfReports": 4
}
```

### Task 2
Create a new type, Compensation. A Compensation has the following fields: employee, salary, and effectiveDate. Create 
two new Compensation REST endpoints. One to create and one to read by employeeId. These should persist and query the 
Compensation from the persistence layer.

The Compensation endpoint expects a data schema like:
```json
    {
        "employee": {
            "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f"
        },
        "salary": 90000,
        "effectiveDate": "2020-03-25T14:56"
    }
```

A get request to http://localhost:8080/api/compensation/{employeeId} will return an array of all compensation types related to that employee in order from most recent to oldest.

```json
[
    {
        "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
        "employee": {
            "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
            "firstName": "John",
            "lastName": "Lennon",
            "position": "Development Manager",
            "department": "Engineering",
            "directReports": null
        },
        "salary": 90000,
        "effectiveDate": "2020-03-25T14:56:00"
    },
    {
        "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
        "employee": {
            "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
            "firstName": "John",
            "lastName": "Lennon",
            "position": "Development Manager",
            "department": "Engineering",
            "directReports": null
        },
        "salary": 90000,
        "effectiveDate": "2020-03-25T14:56:00"
    },
    {
        "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
        "employee": {
            "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
            "firstName": "John",
            "lastName": "Lennon",
            "position": "Development Manager",
            "department": "Engineering",
            "directReports": null
        },
        "salary": 90000,
        "effectiveDate": "2020-03-25T14:56:00"
    },
    {
        "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
        "employee": {
            "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
            "firstName": "John",
            "lastName": "Lennon",
            "position": "Development Manager",
            "department": "Engineering",
            "directReports": null
        },
        "salary": 90000,
        "effectiveDate": "2020-03-25T14:56:00"
    }
]
```
