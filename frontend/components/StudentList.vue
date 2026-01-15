<script setup>
import { ref, onMounted } from "vue";
import { StudentAPI } from "../api/api.js";
import StudentItem from "./StudentItem.vue";
import BaseButton from "./BaseButton.vue";
import { studentIzabran } from "../store/notifications.js";

const students = ref([]);
const selectedStudent = ref(null);
const emit = defineEmits(["select"]);

function selectStudent(student) {
  selectedStudent.value = student;
  studentIzabran();
  emit("select", student);
}

async function loadStudents() {
  const res = await StudentAPI.getAll();
  students.value = res.data;
}

onMounted(loadStudents);

defineExpose({
  loadStudents
});
</script>

<template>
  <div class="student-list">
    <h2>Lista studenata</h2>

    <div class="students-grid">
      <StudentItem
        v-for="s in students"
        :key="s.ID_STUDENTA"
        :student="s"
        :selected="selectedStudent?.ID_STUDENTA === s.ID_STUDENTA"
        @select="selectStudent"
      />
    </div>

    <BaseButton action="GET" @click="loadStudents">
      Osveži listu
    </BaseButton>
  </div>
</template>

<style scoped>
.student-list {
  width: 400px;             
  border-right: 1px solid #ccc;
  padding: 10px;

  height: 95vh;
  overflow-y: auto;
  box-sizing: border-box;
}

.student-list > .students-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 6px;
}


</style>
