import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home.vue'
import Beginning from '@/views/Beginning.vue'

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'Beginning',
        component: Beginning
    },
    {
        path: '/Home',
        name: 'Home',
        component: Home
    }
]

const router = new VueRouter({
    routes
})

export default router