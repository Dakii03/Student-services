<script setup>
defineProps({
  student: Object,
  selected: Boolean
});

defineEmits(["select"]);
</script>

<template>
  <div
    class="student-item"
    :class="{ active: selected }"
    @click="$emit('select', student)"
  >
    <strong>{{ student.IME }} {{ student.PREZIME }}</strong>
    <div class="index">
      {{ student.SMER }}-{{ student.BROJ }}/{{ student.GODINA_UPISA.slice(-2) }}
    </div>
  </div>
</template>

<style scoped>
.student-item {
  padding: 8px;
  margin-bottom: 6px;
  border: 1px solid white;
}

.student-item:hover {
  background: gray;
}

.student-item.active {
  background: #dbeafe;
  border-color: blue;
}

</style>
