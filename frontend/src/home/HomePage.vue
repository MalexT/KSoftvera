<template>
    <div>
        <h1>Hi {{account.user.firstName}}!</h1>
        <p>You're logged in with Vue + Vuex & JWT!!</p>
        <p>
            <router-link to="/login">Logout</router-link>
        </p>
    </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
    computed: {
        ...mapState({
            account: state => state.account
        })
    }
};
</script>