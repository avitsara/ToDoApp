<!--
<section class="vh-100">

  <div class="container py-5 h-100">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-md-8 text-center">
        <h2 class="mb-4">Welcome, {{ username }}</h2>
        <button @click="logout" class="btn btn-danger">Logout</button>
      </div>
    </div>
    <section class="d-flex flex-column py-5" style="height: calc(100vh - 5rem);">
      <div class="container-fluid">
        <div class="row d-flex justify-content-center align-items-start">
          <div class="col-12 col-md-8">
            <div class="card" id="list1" style="border-radius: .75rem; background-color: #eff1f2; height: auto;">
              <div class="card-body py-4 px-4">
                <p class="h1 text-center mt-3 mb-4 pb-3 text-primary">
                  <i class="fas fa-check-square me-1"></i>
                  <u>My Todo-s</u>
                </p>
                <div class="pb-2">
                  <div class="card">
                    <div class="card-body">
                      <div class="d-flex flex-row align-items-center">
                        <input type="text" class="form-control form-control-lg" v-model="newTask.title" placeholder="Add new...">
                        <input type="text" class="form-control form-control-lg ms-3" v-model="newTask.description" placeholder="Description">
                        <a href="#!" data-mdb-tooltip-init title="Set due date">
                          <i class="fas fa-calendar-alt fa-lg me-3"></i>
                        </a>
                        <div>
                          <button type="button" @click="addTask" class="btn btn-primary">Add</button>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <hr class="my-4">
                <div class="d-flex flex-column" style="max-height: 60vh; overflow-y: auto;">
                  <div v-for="task in tasks" :key="task.task_id" class="task-item">
                    <div class="d-flex flex-row align-items-center">
                      <input type="checkbox" class="form-check-input me-2" v-model="task.is_completed">
                      <div class="flex-grow-1">
                        <p class="lead fw-normal mb-0">{{ task.title }}</p>
                        <small>{{ task.description }}</small>
                        <div class="text-muted small">
                          Created: {{ formatDate(task.created_at) }} | Updated: {{ formatDate(task.updated_at) }}
                        </div>
                      </div>
                      <button class="btn btn-sm btn-danger ms-2" @click="deleteTask(task.task_id)">Delete</button>
                    </div>
                    <hr class="my-4">
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</section>
-->

<template>
    <div class="container py-5 h-100">
      <div class="row d-flex justify-content-center align-items-center h-100">
        <div class="col-md-8  
 text-center">
          <h2 class="mb-4">Welcome, {{ username }}</h2>
          <button @click="logout" class="btn btn-danger">Logout</button>
        </div>
      </div>
      <section class="d-flex flex-column py-5" style="height: calc(100vh - 5rem);">
        <div class="container-fluid">
          <div class="row d-flex justify-content-center align-items-start">
            <div class="col-12 col-md-8">
              <div class="card" id="list1" style="border-radius: .75rem; background-color: #eff1f2; height: auto;">
                <div class="card-body py-4 px-4">
                  <h1 class="text-center mt-3 mb-4 pb-3 text-primary">
                    <i class="fas fa-check-square me-1"></i>
                    <u>My Todo-s</u>
                  </h1>

                  <div class="pb-2">
                    <div class="card">
                      <div class="card-body">

                        <div class="input-group">
                          <input type="text" class="form-control form-control-lg" v-model="newTask.title" placeholder="Add new..." aria-label="Add new..." />
                          <input type="text" class="form-control form-control-lg ms-3" v-model="newTask.description" placeholder="Description" aria-label="Description" />
                          <button class="btn btn-primary" @click="addTask">Add</button>
                        </div>
                      </div>
                    </div>
                  </div>

                  <hr class="my-4">

                  <div class="d-flex flex-column" style="max-height: 60vh; overflow-y: auto;">
                    <ul class="list-group list-group-flush">
                      <li v-for="task in tasks" :key="task.task_id" class="list-group-item">
                        <div class="d-flex flex-row align-items-center">
                          <input type="checkbox" class="form-check-input me-2" v-model="task.is_completed">
                          <div class="flex-grow-1">
                            <h5 class="card-title">{{ task.title }}</h5>
                            <p class="card-text">{{ task.description }}</p>
                            <div class="text-muted small">
                              Created: {{ formatDate(task.created_at) }} | Updated: {{ formatDate(task.updated_at) }}
                            </div>
                          </div>
                          <button class="btn btn-sm btn-danger ms-2" @click="deleteTask(task.task_id)">Delete</button>
                        </div>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
</template>

<script>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  import { useRouter } from 'vue-router';

  export default {
    name: 'Home',
    setup() {
      const router = useRouter();
      const username = ref(localStorage.getItem('username') || '');
      const user_id = ref(localStorage.getItem('user_id'));
      const tasks = ref([]);
      const newTask = ref({
        userId: user_id.value,
        title: '',
        description: '',
        is_completed: false
      });

      console.log(newTask);
      const logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        localStorage.removeItem('user_id');
        router.push('/login');
      };

      const fetchTasks = async () => {
        console.log('User ID before API call:', user_id.value); // Check the value

        if (!user_id) {
          console.error('No user_id found in localStorage.');
          router.push('/login');
          return;
        }

        try {
          const response = await axios.get(`https://localhost:7152/api/Task/user/${user_id.value}`); // Use backticks here
          tasks.value = response.data;
          console.log(response.data);
        } catch (error) {
          console.error('Error fetching tasks:', error);
        }
      };


      const addTask = async () => {
        try {
          const response = await axios.post('https://localhost:7152/api/Task/create', newTask.value);
          tasks.value.push(response.data);
          newTask.value.title = '';
          newTask.value.description = '';
        } catch (error) {
          console.error('Error creating task:', error);
          console.log(user_id);
        }
      };

      const formatDate = (dateString) => {
        const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
        return new Date(dateString).toLocaleDateString(undefined, options);
      };

      onMounted(fetchTasks);

      return {
        username,
        logout,
        tasks,
        newTask,
        addTask,
        formatDate
      };
    }
  };
</script>

<style scoped>
  #list1 .form-control {
    border-color: transparent;
  }

    #list1 .form-control:focus {
      border-color: transparent;
      box-shadow: none;
    }

  #list1 .select-input.form-control[readonly]:not([disabled]) {
    background-color: #fbfbfb;
  }

  .card {
    height: auto;
    width: 1500px;
    max-width: 100%;
    margin: 0 auto;
  }
</style>
