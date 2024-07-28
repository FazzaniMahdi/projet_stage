<script setup>
  import NavBar from "./components/NavBar.vue"
  import Footer from "./components/Footer.vue"
  import {ref} from 'vue'
  import store from "./store"
  import { useRouter } from "vue-router"
  const router = useRouter();
  router.beforeEach(async (to, from) => {
    // the second parameter is to avoid an infinite redirect loop
    if (!store.getters.GetToken && to.name == 'UserSettingsView') {
      return { name: 'signIn'}
    }
    if(!store.getters.GetToken && to.name == 'addJobListing') {
      return {name: 'signIn'}
    }
    if(!store.getters.GetToken && to.name == 'updateJobListing') {
      return {name: 'signIn'}
    }
})
</script>

<template>
  <NavBar />
  <!--this is where the component associated with the inserted route will render-->
  <RouterView :key="$route.fullPath"/>
  <Footer />
</template>
