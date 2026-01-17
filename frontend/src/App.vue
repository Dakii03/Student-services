<script setup>
import { ref } from "vue";
import StudentList from "../components/StudentList.vue";
import StudentDetails from "../components/StudentDetails.vue";
import Notification from "../components/Notification.vue";
import ExamStatistics from "../components/ExamStatistics.vue";

const selectedStudent = ref(null);
const listRef = ref(null);

const activeView = ref("students"); // "students" | "statistics"

function refreshAll() {
  listRef.value?.loadStudents();
}
</script>

<template>
  <Notification />

  <nav class="menu">
    <button
      :class="{ active: activeView === 'students' }"
      @click="activeView = 'students'"
    >
      Studenti
    </button>

    <button
      :class="{ active: activeView === 'statistics' }"
      @click="activeView = 'statistics'"
    >
      Statistika
    </button>
  </nav>

  <div v-if="activeView === 'students'" class="layout">
    <StudentList
      ref="listRef"
      @select="s => selectedStudent = s"
    />

    <StudentDetails
      :student="selectedStudent"
      @refresh="refreshAll"
    />
  </div>

  <div v-else-if="activeView === 'statistics'" class="layout">
    <ExamStatistics />
  </div>
</template>



<style>
.menu {
  display: flex;
  gap: 10px;
  padding: 10px;
  border-bottom: 1px solid #ddd;
}

.menu button {
  padding: 8px 14px;
  border: none;
  cursor: pointer;
  background: #eee;
}

.menu button.active {
  background: #3b82f6;
  color: white;
}

.layout {
  display: flex;
}

</style>
