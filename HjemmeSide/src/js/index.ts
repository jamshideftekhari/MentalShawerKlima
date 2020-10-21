import axios, {
    AxiosResponse,
    AxiosError
} from "../../node_modules/axios/index"


interface IToDo {
    "userId": number,
    "id": number,
    "title": string,
    "completed": boolean
}

//localhost temporary
let baseUrl = 'https://apimentalshowerindoor.azurewebsites.net/api/indoorclimate';

new Vue({
    // TypeScript compiler complains about Vue because the CDN link to Vue is in the html file.
    // Before the application runs this TypeScript file will be compiled into bundle.js
    // which is included at the bottom of the html file.
    el: "#app",
    data: {
        humidity: 0
    },
    methods: {
        async getAllToDosAsync() {
            try { return axios.get<IToDo[]>(baseUrl) }
            catch (error: AxiosError) {
                this.message = error.message;
                alert(error.message)
            }
        }

    }
})


