<script>
import axios from 'axios';
import { onMounted, ref } from 'vue';
import store from '@/store';
    export default {
        name: "UserSettingsView",
        setup() {
            const userData = ref({})
            const userName = ref("")
            const email = ref("")
            const jhFullName = ref("")
            const jhResume = ref("")
            const jlName = ref("")
            const jlWebsite = ref("")
            const accountType = ref("")
            onMounted(() => {
                axios.get("https://localhost:7075/Api/Authentication/UserInfo",
                    {
                        headers: {
                            Authorization : `Bearer ${store.getters.GetToken}`
                        }
                    }
                )
                .then(response => {
                    console.log(response.data.token.claims)
                    console.log(response.data.user)
                    userData.value = response.data.token.claims
                    email.value = response.data.token.claims[2]
                    userName.value = response.data.token.claims[3]
                    jhFullName.value = response.data.token.claims[4]
                    jhResume.value = response.data.token.claims[5]
                    jlName.value = response.data.token.claims[6]
                    jlWebsite.value = response.data.token.claims[7]
                    accountType.value = response.data.token.claims[8]
                }
                )
                .catch(err => 
                {
                    console.log(err)
                })
            })

            return {
                userData,
                email,
                userName,
                jhFullName,
                jhResume,
                jlName,
                jlWebsite,
                accountType
            }

        }
    }
</script>
<template>
    <!--This is a placeholder for what is to come-->
    <h1>User info</h1>
    <form>
        <div class="mb-3">
            <label class="form-label">Email</label>
            <input type="Text" class="form-control" v-model="email.value" readonly>
        </div>
        <div class="mb-3">
            <label class="form-label">UserName</label>
            <input type="text" class="form-control" v-model="userName.value" readonly>
        </div>
        <div class="mb-3"  v-if="accountType.value == 'Hunter' && jhFullName.value != ''">
            <label class="form-label">JobHunterFullName</label>
            <input type="text" class="form-control" v-model="jhFullName.value" readonly>
        </div>
        <div class="mb-3"  v-if="accountType.value == 'Hunter' && jhResume.value != ''">
            <label class="form-label">JobHunterResume</label>
            <input type="text" class="form-control" v-model="jhResume.value" readonly>
        </div>
        <div class="mb-3"  v-if="accountType.value == 'Lister' && jlName.value != ''">
            <label class="form-label">JobListerName</label>
            <input type="text" class="form-control" v-model="jlName.value" readonly>
        </div>
        <div class="mb-3"  v-if="accountType.value == 'Lister' && jlWebsite.value != ''">
            <label class="form-label">JobListerWebsite</label>
            <input type="text" class="form-control" v-model="jlWebsite.value" readonly>
        </div>
        <div class="mb-3">
            <label class="form-label">AccountType</label>
            <input type="text" class="form-control" v-model="accountType.value">
        </div>
    </form>
</template>