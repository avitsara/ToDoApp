<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <h2 class="text-center mb-4">Login</h2>
        <form @submit.prevent="submitForm" novalidate>
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <input type="email"
                   class="form-control"
                   id="email"
                   v-model="formData.email"
                   :class="{ 'is-invalid': errors.email }"
                   required
                   placeholder="Enter your email" />
            <div v-if="errors.email" class="invalid-feedback">
              {{ errors.email }}
            </div>
          </div>
          <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <input type="password"
                   class="form-control"
                   id="password"
                   v-model="formData.password"
                   :class="{ 'is-invalid': errors.password }"
                   required
                   placeholder="Enter your password" />
            <div v-if="errors.password" class="invalid-feedback">
              {{ errors.password }}
            </div>
          </div>
          <div class="d-grid gap-2">
            <button type="submit" class="btn btn-primary">Login</button>
          </div>
          <div class="register-link">
            <router-link to="/">Don't have an account? Register here!</router-link>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
  import { ref } from 'vue';
  import axios from 'axios';
  import { useRouter } from 'vue-router';

  export default {
    name: 'Login',
    setup() {
      const router = useRouter();
      const formData = ref({
        email: '',
        password: ''
      });

      const errors = ref({
        email: '',
        password: ''
      });

      const validateForm = () => {
        errors.value = {
          email: '',
          password: ''
        };

        let valid = true;
        if (!formData.value.email) {
          errors.value.email = 'Email is required';
          valid = false;
        } else if (!/\S+@\S+\.\S+/.test(formData.value.email)) {
          errors.value.email = 'Email is invalid';
          valid = false;
        }
        if (!formData.value.password) {
          errors.value.password = 'Password is required';
          valid = false;
        }
        return valid;
      };

      const submitForm = async () => {
        if (validateForm()) {
          try {
            const response = await axios.post('https://localhost:7152/api/User/login', {
              email: formData.value.email,
              password: formData.value.password
            });
            console.log('Login successful:', response.data);
            router.push('/home'); // Redirect to home page or dashboard
          } catch (error) {
            console.error('Error logging in:', error);
          }
        }
      };

      return {
        formData,
        errors,
        submitForm
      };
    }
  };
</script>

<style scoped>
  input {
    width: 500px;
  }
</style>
