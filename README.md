## Project Structure

- **Controllers/**
  - `HomeController.cs` – Default MVC project controller (Nothing changed)
  - `WeatherController.cs` – Weather API call and model deserialization
- **Views/**
  - `Home/`
    - `Index.cshtml` – Main page where weather data is displayed
- **Models/**
  - `WeatherApiResponseModel.cs` – Defines weather data from WeatherApi.com (`current.json` endpoint)
- `appsettings.json` – Configuration file (excluded from Git)
- **wwwroot/**
  - **js/**
    - `search.js` – Quick script from ChatGPT, manages the search button and has some leftover code used for testing.

## Deployment

This project is deployed on [http://deividasbruzas.runasp.net](http://deividasbruzas.runasp.net), thanks to MonsterAsp.net.
