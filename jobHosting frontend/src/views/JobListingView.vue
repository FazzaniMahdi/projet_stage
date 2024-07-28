<script setup>
    import axios from 'axios';
    import { useRoute } from 'vue-router';
    import { computed, getCurrentInstance, onBeforeUnmount, onMounted, reactive, ref,watch } from 'vue';
    import SearchBar from '../components/SearchBar.vue';
    import store from '@/store';
    import router from '@/router';

    const route = useRoute()
    const jobListings = reactive({})
    const searchResults = ref([])
    const currentUserRole = ref("")
    const currentUserId = ref("")
    const MutationWatcher = ref(0)

    const params = route.params;
    const searchTerm = ref("")
    watch(searchTerm, () => {
        Object.values(jobListings).filter(listing => {
            console.log(listing)
        })
    })

    onMounted(() => {
        // for now no idea how to refresh the list after a delete/modify
        // hopefully something will come up late on
        // UPDATE: something DEFINITELY happened to make auto refresh work
        // i just don't have an idea what it is
        // --if it ain't broken don't fix it i guess

        axios.get("https://localhost:7075/api/jobListings/List")
        .then(
            (response) => {
                Object.assign(jobListings, response.data)
            }
        )
        .catch(error => console.log("this machine *hates* your guts\n reason: ",error));

        if(store.getters.GetToken) {
            axios.get("https://localhost:7075/api/Authentication/UserInfo",{
                headers: {
                    Authorization : `Bearer ${store.getters.GetToken}`
                }
            }).then(res => {
                currentUserRole.value = res.data.token.claims[8]
                currentUserId.value = res.data.token.claims[9]
            })
            .catch(err => console.log(err));
        }
        if(params.searchTerm){
            axios.get(`https://localhost:7075/Api/JobListings/${params.searchTerm}`)
            .then(res => {
                searchResults.value = res.data
            })
        }

    })
    function ApplyOperation(type, job) {
        if(job.jobId) {
            if(type == "Modify")
                router.push(`updateJobListing/${job.jobId}`)
            else
                router.push(`deleteJobListing/${job.jobId}`)
        }
        else
            console.log("this jobListing is inexisting")
        MutationWatcher.value++;
    }
    function GoToDetails(id) {
        router.push(`/JobListing/${id}`)
    }
    
</script>
<template>
    <SearchBar />
    <RouterLink class="btn btn-outline-primary" to="/addJobListing" v-if="currentUserRole.value == 'Lister'">addJobListing</RouterLink>
    
    <table class="table"  v-if="Object.keys(jobListings).length > 0" >
        <thead>
            <tr>
                <th scope="col">JobId</th>
                <th scope="col">JobName</th>
                <th scope="col">JobDescription</th>
                <th scope="col">JobLocation</th>
                <th scope="col" colspan="2" v-if="$store.getters.GetToken" style="text-align: center;">Operations</th>
            </tr>
        </thead>
        <tbody v-if="searchResults.length==0">
            <tr v-for="job in jobListings" :key="job">
                <td @click="GoToDetails(job.jobId)">
                    <p v-if="job.jobId">{{ job.jobId }}</p>
                    <p v-else>null</p>
                </td>
                <td @click="GoToDetails(job.jobId)">
                    <p v-if="job.jobName">{{ job.jobName }}</p>
                    <p v-else>null</p>
                </td>
                <td @click="GoToDetails(job.jobId)">
                    <p v-if="job.jobDescription">{{ job.jobDescription }}</p>
                    <p v-else>null</p>
                </td>
                <td @click="GoToDetails(job.jobId)">
                    <p v-if="job.jobLocation">{{ job.jobLocation }}</p>
                    <p v-else>null</p>
                </td>
                <td v-if="$store.getters.GetToken && currentUserId.value==job.jobsListerId">
                    <button @click="ApplyOperation('Modify',job)" class="btn btn-outline-info" >Modify</button>
                </td>
                <td v-else>
                    unavailable
                </td>
                <td v-if="$store.getters.GetToken && currentUserId.value==job.jobsListerId">
                    <button @click="ApplyOperation('Delete',job)" class="btn btn-outline-danger">Delete</button>
                </td>
                <td v-else>
                    unavailable
                </td>
            </tr>
        </tbody>
        <tbody v-else>
            <tr v-for="job in searchResults" :key="job">
                <td @click="GoToDetails(job.jobId)">
                    <p v-if="job.jobId">{{ job.jobId }}</p>
                    <p v-else>null</p>
                </td>
                <td @click="GoToDetails(job.jobId)">
                    <p v-if="job.jobName">{{ job.jobName }}</p>
                    <p v-else>null</p>
                </td>
                <td @click="GoToDetails(job.jobId)">
                    <p v-if="job.jobDescription">{{ job.jobDescription }}</p>
                    <p v-else>null</p>
                </td>
                <td @click="GoToDetails(job.jobId)">
                    <p v-if="job.jobLocation">{{ job.jobLocation }}</p>
                    <p v-else>null</p>
                </td>
                <td v-if="$store.getters.GetToken">
                    <button @click="ApplyOperation('Modify',job)" class="btn btn-outline-info" >Modify</button>
                </td>
                <td v-if="$store.getters.GetToken">
                    <button @click="ApplyOperation('Delete',job)" class="btn btn-outline-danger">Delete</button>
                </td>
            </tr>
        </tbody>
    </table>
    <div v-else class="full-page">
      <div class="content">
        <h1>No listings found.</h1>
        <p>There are currently no listings to display.</p>
        <p>if you are a lister add your listing <RouterLink to="/addjoblisting">here</RouterLink></p>
      </div>
    </div>
    <br>
</template>
<style scoped>
.full-page {
  height: 50vh; /* Set the component height to 100% of the viewport height */
  display: flex;
  justify-content: center; /* Center the content horizontally */
  align-items: center; /* Center the content vertically */
}

</style>