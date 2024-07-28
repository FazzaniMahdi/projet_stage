<script>
    import axios from 'axios'
    import { ref } from 'vue'
    export default {
        name: "SignUpView",
        setup() {
            const email = ref("")
            const password = ref("")
            const confirmPassword = ref("")
            const userType = ref("Hunter")
            const userName = ref("")
            return {
                email,
                password,
                userType,
                userName,
                confirmPassword
            }
        },
        methods: {
            SignUp() {
                if(this.password == this.confirmPassword) {
                    axios.post("https://localhost:7075/api/Authentication/SignUp",{
                        "email": this.email,
                        "password": this.password,
                        "userType": this.userType,
                        "userName": this.userName
                    })
                    .then(res => {
                        this.$router.push("/signIn")
                    })
                    .catch(err => console.log(err));
                }
                else{
                    console.log(`Password mismatch: ${this.password} ${this.confirmPassword}`);
                }
            }
        }
        
    }
</script>

<template>
    <form @submit.prevent="SignUp">
        <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <input type="Text" id="email" class="form-control" v-model="email" placeholder="user@gmail.com">
        </div>
        <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <input type="Text" id="password" class="form-control" v-model="password">
        </div>
        <div class="mb-3">
            <label for="confirmPassword" class="form-label">Confirm Password</label>
            <input type="Text" id="confirmPassword" class="form-control" v-model="confirmPassword">
        </div>
        <div class="mb-3">
            <label for="userName" class="form-label">User Name</label>
            <input type="Text" id="userName" class="form-control" v-model="userName" placeholder="ShareHolder80">
        </div>
        <div class="mb-3">
            <label for="useType" class="form-label">User Type</label>
            <select class="form-select" aria-label="Default select example" id="useType" v-model="userType">
                <option value="Hunter">Job Hunter</option>
                <option value="Lister">Job Lister</option>
            </select>
        </div>
        
        <button type="submit" class="btn btn-outline-primary">Sign Up</button>
    </form>
</template>