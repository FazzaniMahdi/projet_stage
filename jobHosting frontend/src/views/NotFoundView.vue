<script setup>
    import { RouterLink } from "vue-router";
</script>
<template>
    <div class="full-page">
      <div class="content">
        <h1>Sorry! Page not found.</h1>
        <p>The page you're looking for has either been moved or removed from the site.</p>
        <p>please try searching or <RouterLink to="/">start again at the home page</RouterLink></p>
      </div>
    </div>
  </template>
  
  <style scoped>
  .full-page {
    height: 50vh; /* Set the component height to 100% of the viewport height */
    display: flex;
    justify-content: center; /* Center the content horizontally */
    align-items: center; /* Center the content vertically */
  }
  
  </style>