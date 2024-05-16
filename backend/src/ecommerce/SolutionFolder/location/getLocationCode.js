
const { Country, State, City } = require("country-state-city");

const getCountries = () => {
    return Country.getAllCountries().map(country => ({
        label: country.name,
        value: country.isoCode
    }));
};

const getStates = countryCode => {
    return State.getStatesOfCountry(countryCode).map(state => ({
        label: state.name,
        value: state.isoCode
    }));
};

const getCities = (countryCode, stateCode) => {
    return City.getCitiesOfState(countryCode, stateCode).map(city => ({
        label: city.name,
        value: city.name
    }));
};

const methodName = process.argv[2];

switch (methodName) {
    case "getCountries":
        console.log(JSON.stringify(getCountries()));
        break;
    case "getStates":
        const countryStates = getStates(process.argv[3]);
        console.log(JSON.stringify(countryStates));
        break;
    case "getCities":
        const countryCities = getCities(process.argv[3], process.argv[4]);
        console.log(JSON.stringify(countryCities));
        break;
    default:
        process.exit(1);
}
