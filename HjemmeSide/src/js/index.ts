import axios, {
    AxiosResponse,
    AxiosError
} from "../../node_modules/axios/index"
axios.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded'
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';


interface IFan {
    "sensorID": number,
    "roomID": string,
    "temperature": number,
    "humidity": number,
    "cO2": number,
    "pressure": number
}

//localhost temporary
let baseUrl = 'https://apimentalshowerindoor.azurewebsites.net/api/indoorclimate';

new Vue({
    // TypeScript compiler complains about Vue because the CDN link to Vue is in the html file.
    // Before the application runs this TypeScript file will be compiled into bundle.js
    // which is included at the bottom of the html file.
    el: "#app",
    data: {
        fans: [],
        fan: {"humidity": 0},
        sensorId: "1",
        styleObject: {
            background: 'red',
            color: 'white',
            fontSize: '20px'
          }
    },
    methods: {
        async getFanAsync(){
            try {
                //get sensor with id this.sensorId
                axios.get<IFan>(baseUrl + "/" + this.sensorId, {headers: {"Access-Control-Allow-Origin": "*","Access-Control-Allow-Methods" : "GET,PUT,POST,DELETE,PATCH,OPTIONS","Access-Control-Allow-Credentials": "true"} } )
                .then(result => {this.fan = result.data;})
                .catch(error => {return {"humidity": 0}});

            }
            catch ( error: AxiosError){
                this.message = error.message;
                alert(error.message);
            }
        }
    },
    created() {
        this.getFanAsync();
        this.interval = setInterval(() => this.getFanAsync(), 2000);
    }
})

