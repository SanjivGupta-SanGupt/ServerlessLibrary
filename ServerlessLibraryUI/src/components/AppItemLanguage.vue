<template>
            <span v-if="language !== '' && language !== 'na'" class="c-downloads">
            <!-- <svg viewBox="0 0 2048 1048" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" class="c-downloads-icon">
              <path transform="translate(0,2048) scale(1,-1)" d= "M1243 2048L1792 1499L1792 0L128 0L128 2048L1243 2048ZM1280 1829L1280 1536L1573 1536L1280 1829ZM256 128L1664 128L1664 1408L1152 1408L1152 1920L256 1920L256 128ZM659 1197L749 1107L538 896L749 685L659 595L358 896L659 1197ZM1171 1107L1261 1197L1562 896L1261 595L1171 685L1382 896L1171 1107Z"></path>
            </svg> -->
            {{ language | ToDisplayLanguage }}
          </span>
</template>

<script>
export default {
  props: {
    language: {
      type: String,
      required: true
    },
     width: {
      type: Number,
      default: 17
    }
  },
  filters: {
 ToDisplayLanguage(value) {
      if (!value) { return ''; }
      var language = value.toLowerCase();
      if (language === 'csharp') {
        return 'C#';
      } else if (language === 'javascript') {
        return 'JavaScript';
      } else if (language === 'python') {
        return 'Python';
      } else if (language === 'java') {
        return 'Java';
      } else if (language === 'na') {
        return '';
      } else {
        return value;
      }
    }
  }
};
</script>

<style lang="scss">
.functionslogo,
.logicappslogo {  
  vertical-align: baseline;
  margin-right: 4px;
}
</style>

