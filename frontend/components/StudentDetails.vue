<script setup>
import StudentEdit from "./StudentEdit.vue";
import PassedExams from "./PassedExams.vue";
import ExamStatistics from "./ExamStatistics.vue";

defineProps({
  student: Object
});

const emit = defineEmits(["refresh"]);

</script>

<template>
  <div v-if="student" class="details">
    <h2>Detalji studenta</h2>

    <div>
      <p><strong>Ime:</strong> {{ student.IME }}</p>
      <p><strong>Prezime:</strong> {{ student.PREZIME }}</p>
      <p>
        <strong>Broj indeksa:</strong>
        {{ student.SMER }}-{{ student.BROJ }}/{{ student.GODINA_UPISA.slice(-2) }}
      </p>
    </div>

    <StudentEdit
      :student="student"
      @updated="emit('refresh')"
    />
    <PassedExams :studentId="student.ID_STUDENTA" />
    
  </div>

  <div v-else class="empty">
    Izaberite studenta iz liste.
    
  </div>
  <ExamStatistics />
</template>

<style scoped>
.details {
  padding: 15px;
}


.empty {
  padding: 20px;
  color: gray;
  font-style: italic;
}
</style>
