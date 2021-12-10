export default {
    fontAwesomeConfig() {
        import { library } from '@fortawesome/fontawesome-svg-core'
        import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
        import { faEllipsisH } from '@fortawesome/free-solid-svg-icons'

        library.add(faEllipsisH)

        Vue.component('font-awesome-icon', FontAwesomeIcon)

    }
}