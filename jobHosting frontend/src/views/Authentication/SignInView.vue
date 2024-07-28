<script>
    import axios from 'axios'
    import {ref} from 'vue'
    import { useStore } from 'vuex'
    export default {
        name: "SignInView",
        setup() {
            const userName = ref("")
            const pwd = ref("")
            const rememberMe = ref(false);
            const token = ref("")
            const store = useStore();

            function GetToken(data) {
                token.value = data;
                store.commit("MutateToken",data)
            }
            return {
                userName,
                pwd,
                token,
                rememberMe,
                store,
                GetToken
            }
        },
        methods: {
            authenticate() {
                axios.post("https://localhost:7075/Api/Authentication/SignIn", {
                    "userName": this.userName,
                    "password": this.pwd
                }) 
                .then(response => 
                {
                    this.GetToken(response.data.token)
                    this.$router.push("/");
                })
                .catch(error => {
                    console.log(error)
                })

            }
        }
    }

</script>
<template>
    <form @submit.prevent="authenticate">
        <div class="mb-3">
            <label for="userName" class="form-label">userName</label>
            <input type="Text" class="form-control" id="userName" v-model="userName">
        </div>
        <div class="mb-3">
            <label for="Password" class="form-label">Password</label>
            <input type="Text" class="form-control" id="Password" v-model="pwd">
        </div>
        <div class="mb-3 form-check">
            <input type="checkbox" class="form-check-input" v-model="rememberMe" id="rememberMe">
            <label class="form-check-label" for="rememberMe">Remember Me</label>
        </div>
        <button type="submit" class="btn btn-outline-primary">login</button>
    </form>
</template>