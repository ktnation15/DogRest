const url = 'https://dogrest20240607130248.azurewebsites.net/api/Dogs'

Vue.createApp({
    data() {
        return {
            Dogs: [],
            Dog: {
                id: 0,
                Name: "",
                Age: 0
            },
            GetDog: {
                id: 0,
                Name: "",
                Age: 0
            },
            deleteId: 0,
            dogIdToGet: null,
        }
    },
    async created() {
        await this.GetDogs();
    },
    methods: {
        GetDogs() {
            // Add your logic for getting all dogs here
            axios.get(url)
                .then(response => {
                    this.Dogs = response.data;
                })
                .catch(error => {
                    console.error(error);
                });
        },
        async getById(id) {
            try {
                const response = await axios.get(url + '/' + id);
                this.GetDog = response.data; // directly assign the response data
            } catch (error) {
                console.error(error);
                // Add your error handling logic here
            }
        },
        addDog() {
            // Add your logic for adding a dog here
            this.Dogs.push({
                id: this.Dog.Id,
                name: this.Dog.Name,
                age: this.Dog.Age
            });
            this.Dog.Id = '';
            this.Dog.Name = '';
            this.Dog.Age = '';
        },
        async deleteDog(id) {
            const response = await axios.delete(url + '/' + id);
            console.log(response);
            this.GetDogs();
        }
    }
}).mount('#app')