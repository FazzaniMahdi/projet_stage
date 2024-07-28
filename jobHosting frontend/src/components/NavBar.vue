<template>
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
        <div class="container-fluid">
            <RouterLink class="navbar-brand" to="/">
                <img src="shuffle.png" width="30" height="30" class="d-inline-block align-top" alt="logo"><span style="text-decoration: underline;">ShuffleJob</span>
            </RouterLink>

            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <!--literally changed the me-auto to ms-auto (s is for start which makes no sense but it works)-->
                <ul class="navbar-nav me-auto mb-2 mb-lg-0 ml-auto">
                    <li class="nav-item">
                        <RouterLink class="nav-link active" aria-current="page" to="/jobListings">jobListings</RouterLink>
                    </li>
                </ul>
                <ul class="navbar-nav ms-auto mb-2 mb-lg-0 ml-auto">
                    <li class="nav-item" v-if="!this.$store.getters.GetToken">
                        <RouterLink class="nav-link active" aria-current="page" to="/signIn">signIn</RouterLink>
                    </li>
                    <li class="nav-item" v-if="!this.$store.getters.GetToken">
                        <RouterLink class="nav-link active" aria-current="page" to="/signUp">signUp</RouterLink>
                    </li>
                    <li class="nav-item" v-if="this.$store.getters.GetToken">
                        <RouterLink class="nav-link active" aria-current="page" to="/UserSettings">Profile</RouterLink>
                    </li>
                    <li class="nav-item" v-if="this.$store.getters.GetToken">
                        <RouterLink class="nav-link active" aria-current="page" to="/signOut">signOut</RouterLink>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    
</template>