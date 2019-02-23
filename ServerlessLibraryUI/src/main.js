import Vue from 'vue'
import App from './App.vue'
import Home from './Home.vue'
import Contribute from './Contribute.vue'
import VueRouter from 'vue-router'
Vue.use(VueRouter)
const router = new VueRouter({

  routes: [
    {
      path: '/contribute',
      component: Contribute,
    },
    {
      path: '*',
      component: App,
      props: (route) => ({
                  language: route.query.language,
                  type: route.query.type,
                  filtertext: route.query.filtertext
              })
    }
  ]
  
  
});
Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(Home)
}).$mount('#app')
