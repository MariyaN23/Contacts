import { createApp } from 'vue'
import App from './App.vue'
import './main.css'
import PrimeVue from 'primevue/config'
import Aura from '@primeuix/themes/aura'
import 'primeicons/primeicons.css'
import ToastService from 'primevue/toastservice'
import {ConfirmationService} from 'primevue'

const app = createApp(App)
app.use(PrimeVue, {
    theme: {
        preset: Aura,
        options: {
            darkModeSelector: false,
        }
    }
})
app.use(ToastService)
app.use(ConfirmationService)

app.mount('#app')