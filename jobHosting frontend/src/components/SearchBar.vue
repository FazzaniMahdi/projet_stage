<script>
    import {ref} from "vue";
    import router from '@/router';
    export default {
        name: "SearchBar",
        setup(){
            const searchTerm = ref("")
            function Search() {
                router.replace(`jobListings/${searchTerm.value}`)
            }
            return {
                searchTerm,
                Search
            }
        }
    }
</script>
<template>
    <nav class="navbar bg-body-tertiary">
        <div class="container-fluid">
            <form class="d-flex" role="search" @submit.prevent="Search">
                <input class="form-control me-2" type="search" placeholder="Position title" aria-label="PositionSearch" v-model="searchTerm">
                <button class="btn btn-outline-success" type="submit">Search</button>
            </form>
        </div>
    </nav>
</template>