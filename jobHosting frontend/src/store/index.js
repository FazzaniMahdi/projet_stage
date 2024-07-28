import { createStore } from "vuex";
import {ref} from 'vue'
const store = createStore({
    state: {
        token: ref({}),
        test: "this is a test"
    },
    getters: {
        GetToken(state) {
            return state.token.value;
        },
        GetTest(state){
            return state.test;
        }
    },
    mutations: {
        MutateToken(state, data) {
            state.token.value = data
        },
        MutateTest(state, data) {
            state.test = data
        }
    },
    actions: {}
});
export default store;