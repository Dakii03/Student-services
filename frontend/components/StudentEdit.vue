<script setup>
import { reactive, watch } from "vue";
import { StudentAPI } from "../api/api.js";
import BaseButton from "./BaseButton.vue";
import { notifySuccess, notifyError } from "../store/notifications.js";


const props = defineProps({
  student: Object
});

const emit = defineEmits(["updated"]);

const form = reactive({
  ime: "",
  prezime: ""
});

watch(
  () => props.student,
  (s) => {
    if (s) {
      form.ime = s.IME;
      form.prezime = s.PREZIME;
    }
  },
  { immediate: true }
);

async function save() {
  if (!form.ime || !form.prezime) {
    notifyError("Ime i prezime su obavezni");
    return;
  }

  try {
    await StudentAPI.update(props.student.ID_STUDENTA, {
      IME: form.ime,
      PREZIME: form.prezime
    });

    notifySuccess("Podaci uspešno izmenjeni");
    emit("updated");
  } catch (err) {
    notifyError("Greška pri izmeni podataka");
    console.error(err);
  }
}
</script>

<template>
  <div v-if="student" class="edit-box">
    <h3>Izmena podataka studenta</h3>

    <label>Ime</label>
    <input v-model="form.ime" />

    <label>Prezime</label>
    <input v-model="form.prezime" />

    <BaseButton action="PUT" @click="save">
      Sačuvaj izmene
    </BaseButton>
  </div>
</template>

<style scoped>
.edit-box {
  margin-top: 20px;
}

label {
  display: block;
  margin-top: 8px;
}

input {
  width: 100%;
  padding: 6px;
}
</style>
