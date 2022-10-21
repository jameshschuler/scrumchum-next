<template>
  <div class="columns is-mobile">
    <div class="column is-8 is-offset-2">
      <div class="card">
        <div class="card-content">
          <div class="columns is-mobile is-5 is-variable py-6">
            <div class="column is-half px-6">
              <figure class="image">
                <img :src="landingPageImage" />
              </figure>
            </div>
            <div class="column is-half px-6">
              <LandingPageTabs />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import landingPageImage from '@/assets/landing-page.svg';
import LandingPageTabs from '@/components/LandingPageTabs.vue';
</script>
<style lang="scss" scoped></style>
