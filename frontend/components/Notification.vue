<script setup>
import { notification, clearNotification } from "../store/notifications.js";
</script>

<template>
  <div
    v-if="notification.show"
    class="notification"
    :class="notification.type"
  >
    <span>{{ notification.message }}</span>
    <button @click="clearNotification">X</button>
  </div>
</template>

<style scoped>
.notification {
  position: fixed;
  top: 20px;
  right: 20px;
  padding: 12px 16px;
  border-radius: 6px;
  color: black;
  display: flex;
  align-items: center;
  gap: 10px;
  z-index: 1000;
}

.success {
  background: lightgreen;
}

.error {
  background: red;
}

.info {
  background: aqua;
}

button {
  background: transparent;
  border: none;
  color: white;
  font-size: 16px;
}
</style>
