<script>
    import axios from 'axios'
    import { onMounted,ref, watch } from 'vue'
    import { useRoute,useRouter } from 'vue-router'
    import moment from 'moment'
    import store from '@/store'
    export default {
        name: "UpdatJobListingView",
        setup() {
            const route = useRoute()
            const router = useRouter()
            const jobListing = ref({})
            const jobId = route.params.jobListingId;
            const JobsLister = ref({})
            onMounted(() => {
                // Gettings the user in question
                axios.get("https://localhost:7075/Api/Authentication/UserInfo",
                {
                    headers: {
                        Authorization: `Bearer ${store.getters.GetToken}`
                    }
                })
                .then(res =>
                {
                    JobsLister.value = res.data.user
                });
                // Gettings the corresponding job listing
                axios.get(`https://localhost:7075/api/jobListings/${jobId}`)
                .then(response => {
                    jobListing.value = response.data;
                    console.log(response.data.expirationDate)
                    jobListing.value.expirationDate = moment(jobListing.value.expirationDate).format('YYYY-MM-DD');
                })
                .catch(error => console.log(`this machine hates you, reason: ${error}`))
            })
            return {
                jobListing,
                jobId,
                router
            }
        },
        methods: {
            UpdateData() {
                axios.put(`https://localhost:7075/api/jobListings/${this.jobId}`, 
                {
                    "JobName": this.jobListing.jobName,
                    "JobDescription": this.jobListing.jobDescription,
                    "ExpirationDate": this.jobListing.ExpirationDate,
                    "JobRequirements": this.jobListing.jobRequirements,
                    "Missions": this.jobListing.missions,
                    "JobLocation": this.jobListing.jobLocation,
                    "JobHourlyPay": this.jobListing.jobHourlyPay,
                    "JobType": this.jobListing.jobType,
                    "JobPositionsAvailable": this.jobListing.jobPositionsAvailable
                },
                {
                    headers:{
                        Authorization: `Bearer ${store.getters.GetToken}`
                    }
                })
                .then(res => 
                {
                    console.log(res.data)
                    this.$router.push('/jobListings')
                })
                .catch(err => console.log(err))
            }
        }
    }
</script>

<template>
    <h1>Updating JobListing {{ jobListing.jobId }}</h1>
    <form @submit.prevent="UpdateData">
        <div class="mb-3">
            <label for="jobsName" class="form-label">Job Name</label>
            <input type="Text" class="form-control" id="jobsName" v-model="jobListing.jobName" placeholder="name example">
        </div>
        <div class="mb-3">
            <label for="jobsDesc" class="form-label">Job's Description</label>
            <input type="Text" class="form-control" id="jobsDesc" v-model="jobListing.jobDescription" placeholder="description example">
        </div>
        <div class="mb-3">
            <label for="jobsExpDat" class="form-label">Expiration Date</label>
            <input type="date" class="form-control" id="jobsExpDat" v-model="jobListing.expirationDate" placeholder="Expiration date">
        </div>
        <div class="mb-3">
            <label for="jobsLoc" class="form-label">Job's Location</label>
            <input type="Text" class="form-control" id="jobsLoc" v-model="jobListing.jobLocation" placeholder="location example">
        </div>
        <div class="mb-3">
            <label for="jobsMissions" class="form-label">Job's Missions</label>
            <input type="Text" class="form-control" id="jobsMissions" v-model="jobListing.missions" placeholder="Missions example">
        </div>
        <div class="mb-3">
            <label for="jobsRequirements" class="form-label">Job's Requirements</label>
            <input type="Text" class="form-control" id="jobsRequirements" v-model="jobListing.jobRequirements" placeholder="Requirements example">
        </div>
        <div class="mb-3">
            <label for="jobsTyp" class="form-label">Job Type</label>
            <input type="Text" class="form-control" id="jobsTyp" v-model="jobListing.jobType" placeholder="Job type example">
        </div>
        <div class="mb-3">
            <label for="jobsHrPay" class="form-label">Job's Hourly Pay</label>
            <input type="Text" class="form-control" id="jobsHrPay" v-model="jobListing.jobHourlyPay" placeholder="Hourly pay example">
        </div>
        <div class="mb-3">
            <label for="jobsNbPosAvb" class="form-label">Numbre Of Positions Available</label>
            <input type="Text" class="form-control" id="jobsNbPosAvb" v-model="jobListing.jobPositionsAvailable" placeholder="Number Of Positions Available Example">
        </div>
        
        <button type="submit" class="btn btn-outline-primary">submit</button>
    </form>
</template>