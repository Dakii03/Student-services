<script setup>
import { ref } from "vue";
import StudentList from "../components/StudentList.vue";
import StudentDetails from "../components/StudentDetails.vue";
import Notification from "../components/Notification.vue";

const selectedStudent = ref(null);
const listRef = ref(null);

function refreshAll() {
  listRef.value?.loadStudents();
}
</script>

<template>
  <Notification />
  <div class="layout">
    <StudentList
      ref="listRef"
      @select="s => selectedStudent = s"
    />

    <StudentDetails
      :student="selectedStudent"
      @refresh="refreshAll"
    />
  </div>
</template>


<style>
.layout {
  display: flex;
}
</style>
