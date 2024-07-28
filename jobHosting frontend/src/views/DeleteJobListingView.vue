<script>
    import axios from 'axios'
    import { useRoute } from 'vue-router'
    import { onMounted,ref } from 'vue'
    import router from '@/router'
    import store from '@/store'

    export default {
        name: "DeleteJobListingView",
        setup() {
            const route = useRoute();
            const jobId = route.params.jobListingId;
            let jobListing = ref({})
            onMounted(() => {
                axios.get(`https://localhost:7075/Api/JobListings/${jobId}`)
                .then(response => jobListing.value = response.data);
                axios.get("https://localhost:7075/Api/Authentication/UserInfo",
                {
                    headers: {
                        Authorization: `Bearer ${store.getters.GetToken}`
                    }
                })
                .then(res => console.log(res.data));
            })
            return {
                route,
                jobId,
                jobListing
            }
            
        },
        methods: {
            DeleteJobListing() {
                console.log(this.jobId)
                axios.delete(`https://localhost:7075/Api/JobListings/${this.jobId}`,
                    {
                        headers: {
                            Authorization : store.getters.GetToken
                        }
                    }
                )
                .then(response => 
                {
                    console.log(response.data)
                    router.push(`/jobListings`)
                })
                .catch(err => console.log(err));
            }
        }
        
    }
</script>
<template>
    <div class="container mt-5">
    <h1>Are you sure you want to Delete this jobListing?</h1>
    <div class="row mt-4">
      <div class="col-6">
        <button class="btn btn-outline-primary btn-block" @click="DeleteJobListing">Confirm</button>
      </div>
      <div class="col-6">
        <button class="btn btn-outline-danger btn-block" @click="$router.push('/')">Cancel</button>
      </div>
    </div>
  </div>
</template>
<style scoped>
.container {
  text-align: center;
}

.btn-block {
  width: 100%;
}
</style>