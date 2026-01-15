<script setup>
import { computed } from "vue";

const props = defineProps({
  action: {
    type: String,
    required: true // GET, POST, PUT, DELETE
  },
  disabled: Boolean
});

const classes = computed(() => {
  return {
    btn: true,
    get: props.action === "GET",
    post: props.action === "POST",
    put: props.action === "PUT",
    delete: props.action === "DELETE"
  };
});
</script>

<template>
  <button :class="classes" :disabled="disabled">
    <slot />
  </button>
</template>

<style scoped>
.btn {
  padding: 6px 12px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-weight: 600;
}


.get {
  background: #0ea5e9;
  color: white;
}

.post {
  background: #22c55e;
  color: white;
}

.put {
  background: #f59e0b;
  color: white;
}

.delete {
  background: #ef4444;
  color: white;
}

.btn:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}
</style>
