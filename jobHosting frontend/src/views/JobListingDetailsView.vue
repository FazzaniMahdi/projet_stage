<script setup>
    import axios from "axios";
    import { onMounted, ref } from "vue";
    import { useRoute } from "vue-router";
    import moment from 'moment'

    const route = useRoute()
    const listingId = route.params.jobListingId
    const listing = ref({})
    const listerId = ref("")
    const lister = ref({})
    onMounted(() => {
        axios.get(`https://localhost:7075/Api/Joblistings/${listingId}`)
            .then(res => {
                listerId.value = res.data.jobsListerId
                listing.value = res.data
                listing.value.expirationDate = moment(listing.value.expirationDate).format('YYYY-MM-DD');
                if(listerId.value)
                {
                    axios.get(`https://localhost:7075/Api/Authentication/Users/${listerId.value}`)
                    .then(response => lister.value = response.data)
                }
        }).catch(err => console.log(err));
    }) 
</script>
<template>
    <!--
        i've yet to add the requirements and missions
        sugar honey ice tea
    -->
    <div class="mb-3">
        <label class="form-label">Position Name</label>
        <input type="Text" class="form-control" v-model="listing.jobName" readonly>
    </div>
    <div class="mb-3">
        <label class="form-label">Description</label>
        <input type="Text" class="form-control" v-model="listing.jobDescription" readonly>
    </div>
    <div class="mb-3">
        <label class="form-label">Expiration date</label>
        <input type="date" class="form-control" v-model="listing.expirationDate" readonly>
    </div>
    <div class="mb-3">
        <label class="form-label">Location</label>
        <input type="text" class="form-control" v-model="listing.jobLocation" readonly>
    </div>
    <div class="mb-3">
        <label class="form-label">Type</label>
        <input type="text" class="form-control" v-model="listing.jobType" readonly>
    </div>
    <div class="mb-3">
        <label class="form-label">Positions available</label>
        <input type="text" class="form-control" v-model="listing.jobPositionsAvailable" readonly>
    </div>
    <div class="mb-3">
        <!--make this into a link which goes to the details of a user-->
        <label class="form-label">Job Lister</label>
        <input type="text" class="form-control" v-model="lister.userName" readonly>
    </div>
    
</template>