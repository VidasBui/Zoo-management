# Zoo management API in .Net

## Getting Started

 - Make sure that you have .NET Core SDK installed
 - Installation:
    - Clone the repository.
    - Navigate to the project directory.
    - Run dotnet restore to install dependencies.
    - Run dotnet ef database update to create the database.
    - Execute dotnet run to start the API.
    -  Upload enclosures.json to `/api/upload/enclosures`
    - Upload animals.json to `/api/upload/animals`

## Endpoints

- POST `/api/upload/enclosures` - initial enclosures files upload 
- POST `/api/upload/animals` - initial animals files upload 

- POST `/api/animals` - add animal
- DELETE `/api/animals/{animalGuid}` - remove animal
  
## System rules

- Vegetarian animals can be placed together in the same
enclosure.
- Animals of the same species should not be separated and
should be assigned to the same enclosure.
- Meat-eating animals of different species should preferably not
be grouped together in the same enclosure. However, if
necessary due to limited enclosures, only two different species
of meat-eating animals can be grouped together.
### Additional rules:
- Optimal enclosures sizes are set to:
  + Small: 3
  + Medium: 3
  + Large: 8
  + Huge: 15

- Animals are placed to enclosures based on availability index which is calculated by formula:

   `(enclosureSize - assignedAnimalCount) / enclosureSize`
