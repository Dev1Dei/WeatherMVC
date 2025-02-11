
    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById("searchBtn").addEventListener("click", function () {
            let city = document.getElementById("cityInput").value.trim();
            if (!city) {
                alert("Please enter a city name.");
                return;
            }

            fetch("/Weather/GetWeatherInCity", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body: `city=${encodeURIComponent(city)}`
            })
                .then(response => response.json())
                .then(data => {
                    let resultDiv = document.getElementById("weatherResult");

                    if (data.error) {
                        resultDiv.innerHTML = `<div class="alert alert-danger">City not found.</div>`;
                    } else {
                        resultDiv.innerHTML = `
                        <div class="card text-dark bg-light mt-3">
                            <div class="card-header text-center">
                                <h3>${data.locationName}, ${data.country}</h3>
                                <p class="small text-muted">${data.localtime}</p>
                            </div>
                            <div class="card-body text-center">
                                <img src="https:${data.current.condition.icon}" alt="${data.condition}" class="mb-2">
                                <h4>${data.condition}</h4>
                                <p class="display-4"><strong>${data.temperature}°C</strong></p>
                            </div>
                            <div class="card-footer text-center">
                                <p>💨 Wind: ${data.windSpeed} kph</p>
                                <p>🌫️ AQI: ${data.airQuality} (PM2.5)</p>
                            </div>
                        </div>
                    `;
                    }
                })
                .catch(error => {
                    document.getElementById("weatherResult").innerHTML = `<div class="alert alert-danger">Error fetching weather data.</div>`;
                    console.error("Fetch error:", error);
                });
        });
    });
