import { createRouter, createWebHashHistory } from 'vue-router'
import LandingPageView from '@/views/LandingPageView.vue'
import JobListingView from '@/views/JobListingView.vue'
import AddJobListingView from '@/views/AddJobListingrView.vue'
import DeleteJobListingView from '@/views/DeleteJobListingView.vue'
import NotFoundView from '@/views/NotFoundView.vue'
import UpdateJobListingView from '@/views/UpdateJobListingView.vue'
import SignInView from '@/views/Authentication/SignInView.vue'
import SignUpView from '@/views/Authentication/SignUpView.vue'
import UserSettingsView from '@/views/Authentication/UserSettingsView.vue'
import SignOutView from '@/views/Authentication/SignOutView.vue'
import JobListingDetailsView from '@/views/JobListingDetailsView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: LandingPageView
  },
  {
    path: '/jobListings',
    name: 'jobListings',
    component: JobListingView
  },
  {
    path: '/jobListings/:searchTerm',
    name: 'jobListingsParams',
    component: JobListingView
  },
  {
    path: "/addJobListing",
    name: "addJobListing",
    component: AddJobListingView
  },
  {
    path: "/deleteJobListing/:jobListingId",
    name: "deleteJobListing",
    component: DeleteJobListingView
  },
  {
    path: "/updateJobListing/:jobListingId",
    name: "updateJobListing",
    component: UpdateJobListingView
  },
  {
    path: "/signIn",
    name: "signIn",
    component: SignInView
  },
  {
    path: "/signUp",
    name: "signUp",
    component: SignUpView
  },
  {
    path: "/signOut",
    name: "signOut",
    component: SignOutView
  },
  {
    path: "/UserSettings",
    name: "UserSettingsView",
    component: UserSettingsView
  },
  {
    path: "/JobListing/:jobListingId",
    name: "jobListingDetails",
    component: JobListingDetailsView
  },
  {
    path: "/:pathMatch(.*)*",
    name: "notFound",
    component: NotFoundView
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
