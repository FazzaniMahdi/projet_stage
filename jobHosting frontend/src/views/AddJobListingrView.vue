<script>
    import axios from 'axios';
    import { ref } from 'vue';
import store from '@/store';
    export default {
        name: "AddJobListingView",
        data() {
            return{
                JobName: "",
                JobDescription: "",
                ExpirationDate: new Date().toLocaleDateString('en-CA'),
                JobLocation : "",
                Missions: "",
                JobRequirements: "",
                JobType: "",
                JobHourlyPay: 0.0,
                JobPositionsAvailable: 0,
                JobsListerId: "",
                JobsLister: {}
            }
        },
        methods: {
            postData() {
                axios.post("https://localhost:7075/Api/JobListings/MakeListing",
                {
                    "JobName": this.JobName,
                    "JobDescription": this.JobDescription,
                    "ExpirationDate": this.ExpirationDate,
                    "JobRequirements": [
                        "requirement1",
                        "requirement2"
                    ],
                    "Missions":[ 
                        "Mission1",
                        "Mission2"
                    ],
                    "JobLocation": this.JobLocation,
                    "JobType": this.JobType,
                    "JobHourlyPay": this.JobHourlyPay,
                    "JobPositionsAvailable": this.JobPositionsAvailable,
                    "jobsListerId": this.JobsListerId,
                },
                {
                    headers: {
                        Authorization: `Bearer ${store.getters.GetToken}`
                    }
                }
            ).then(response => 
            {
                console.log (response.data)
                this.$router.push(`/jobListings`)
            })
            .catch(error => console.log(error))
            }
        },
        computed: {
            convertHrPay() {
                console.log(typeof(this.jobListingInfo.jobsHrPay))
                if(typeof(this.jobListingInfo.jobsHrPay) != "number")
                    parseFloat(this.jobListingInfo.jobsHrPay)
            },
            convertNbPositions() {
                if(typeof(this.jobListingInfo.jobsNbPosAvb) != "number")
                    parseInt(this.jobListingInfo.jobsNbPosAvb)
            }
        },
        mounted() {
            axios.get("https://localhost:7075/Api/Authentication/UserInfo", {
                headers: {
                    Authorization : `Bearer ${store.getters.GetToken}`
                }
            })
            .then(response => {
                console.log(response)
                this.JobsListerId = response.data.token.claims[9].value
                this.JobsLister = response.data.user
            })
            .catch(err => console.log(err));
        }
    }
</script>
<template>
    <form @submit.prevent="postData">
        <div class="mb-3">
            <label for="jobsName" class="form-label">Job Name</label>
            <input type="Text" class="form-control" id="jobsName" v-model="JobName" placeholder="name example">
        </div>
        <div class="mb-3">
            <label for="jobsDesc" class="form-label">Job's Description</label>
            <input type="Text" class="form-control" id="jobsDesc" v-model="JobDescription" placeholder="description example">
        </div>
        <div class="mb-3">
            <label for="jobsExpDat" class="form-label">Expiration Date</label>
            <input type="date" class="form-control" id="jobsExpDat" v-model="ExpirationDate" placeholder="Expiration date">
        </div>
        <div class="mb-3">
            <label for="jobsLoc" class="form-label">Job's Location</label>
            <input type="Text" class="form-control" id="jobsLoc" v-model="JobLocation" placeholder="location example">
        </div>
        <div class="mb-3">
            <label for="jobsMissions" class="form-label">Job's Missions</label>
            <input type="Text" class="form-control" id="jobsMissions" v-model="Missions" placeholder="Missions example">
        </div>
        <div class="mb-3">
            <label for="jobsRequirements" class="form-label">Job's Requirements</label>
            <input type="Text" class="form-control" id="jobsRequirements" v-model="JobRequirements" placeholder="Requirements example">
        </div>
        <div class="mb-3">
            <label for="jobsTyp" class="form-label">Job Type</label>
            <input type="Text" class="form-control" id="jobsTyp" v-model="JobType" placeholder="Job type example">
        </div>
        <div class="mb-3">
            <label for="jobsHrPay" class="form-label">Job's Hourly Pay</label>
            <input type="Text" class="form-control" id="jobsHrPay" v-model="JobHourlyPay" placeholder="Hourly pay example">
        </div>
        <div class="mb-3">
            <label for="jobsNbPosAvb" class="form-label">Numbre Of Positions Available</label>
            <input type="Text" class="form-control" id="jobsNbPosAvb" v-model="JobPositionsAvailable" placeholder="Number Of Positions Available Example">
        </div>
        <button type="submit" class="btn btn-outline-primary">submit</button>
    </form>
</template>