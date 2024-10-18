<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6 text-center">
        <h2 class="mb-4">Welcome, {{ username }}</h2>
        <button @click="logout" class="btn btn-danger">Logout</button>
      </div>
    </div>
  </div>
</template>

<script>
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';

  export default {
    name: 'Home',
    setup() {
      const router = useRouter();
      const username = ref(localStorage.getItem('username') || '');

      const logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        router.push('/login'); 
      };

      return {
        username,
        logout
      };
    }
  };
</script>

<style scoped>
  .container {
    margin-top: 50px;
  }
</style>
