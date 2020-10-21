import axios, {
    AxiosResponse,
    AxiosError
} from "../../node_modules/axios/index"


interface IFan {
    "SensorID": number,
    "RoomID": string,
    "Temperature": number,
    "Humidity": number,
    "CO2": number,
    "Presure": number
}

//localhost temporary
let baseUrl2 = 'https://apimentalshowerindoor.azurewebsites.net/api/indoorclimate';
let baseUrl = 'http://jsonplaceholder.typicode.com/todos';

new Vue({
    // TypeScript compiler complains about Vue because the CDN link to Vue is in the html file.
    // Before the application runs this TypeScript file will be compiled into bundle.js
    // which is included at the bottom of the html file.
    el: "#app",
    data: {
        toDos: [],
        humidity: 0
    },
    methods: {
        async getAllToDos() {
            let response = await this.getAllToDosAsync();
            this.toDos = response.data;
        },
        async getAllToDosAsync() {
            try { return axios.get<IFan[]>(baseUrl2) }
            catch (error: AxiosError) {
                this.message = error.message;
                alert(error.message)
            }
        }

    }
})


