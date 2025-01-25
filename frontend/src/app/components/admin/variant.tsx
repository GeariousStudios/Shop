import React, { Dispatch, FC, SetStateAction, useState } from "react";
import styles from "./variant.module.css";

type VariantProps = {
  variants: { title: string; options: string[] }[];
  setVariants: Dispatch<SetStateAction<{ title: string; options: string[] }[]>>;
};

const Variant: FC<VariantProps> = ({ variants, setVariants }) => {
  const [combinations, setCombinations] = useState<string[][]>([]);
  const [showTable, setShowTable] = useState(false);

  const handleAddVariant = () => {
    setVariants((prevVariants) => [...prevVariants, { title: "", options: [] }]);
  };

  const handleVariantTitleChange = (index: number, title: string) => {
    setVariants((prevVariants) => prevVariants.map((variant, i) => (i === index ? { ...variant, title } : variant)));
  };

  const handleAddOption = (index: number, option: string) => {
    setVariants((prevVariants) => {
      const updatedVariants = prevVariants.map((variant, i) =>
        i === index ? { ...variant, options: [...variant.options, option] } : variant
      );

      const newCombinations = generateCombinations(updatedVariants);
      setCombinations(newCombinations);

      if (!showTable && newCombinations.length > 0) {
        setShowTable(true);
      }

      return updatedVariants;
    });
  };

  const handleRemoveVariant = (index: number) => {
    setVariants((prevVariants) => {
      const updatedVariants = prevVariants.filter((_, i) => i !== index);

      const newCombinations = generateCombinations(updatedVariants);
      setCombinations(newCombinations);

      if (newCombinations.length === 0) {
        setShowTable(false);
      }

      return updatedVariants;
    });
  };

  const handleRemoveOption = (variantIndex: number, optionIndex: number) => {
    setVariants((prevVariants) => {
      const updatedVariants = prevVariants.map((variant, i) =>
        i === variantIndex
          ? {
              ...variant,
              options: variant.options.filter((_, idx) => idx !== optionIndex),
            }
          : variant
      );

      const newCombinations = generateCombinations(updatedVariants);
      setCombinations(newCombinations);

      if (newCombinations.length === 0) {
        setShowTable(false);
      }

      return updatedVariants;
    });
  };

  const generateCombinations = (variants: { title: string; options: string[] }[]) => {
    const validVariants = variants.filter((variant) => variant.options.length > 0);
    if (validVariants.length === 0) return [];

    let combinations = validVariants[0].options.map((option) => [option]);

    for (let i = 1; i < validVariants.length; i++) {
      const newCombinations: string[][] = [];
      combinations.forEach((combo) => {
        validVariants[i].options.forEach((option) => {
          newCombinations.push([...combo, option]);
        });
      });
      combinations = newCombinations;
    }

    return combinations;
  };

  return (
    <div className={styles.inputContainer}>
      {variants.map((variant, index) => (
        <div key={index} className={styles.variantRow}>
          <div className={styles.inputGroup}>
            {/* Variant Title */}
            <input
              type="text"
              placeholder="Ange valets titel (t.ex. Färg)"
              value={variant.title}
              onChange={(e) => handleVariantTitleChange(index, e.target.value)}
            />
            {/* Combined Input Field for Options */}
            <div className={styles.tagsInput}>
              {variant.options.map((option, i) => (
                <span key={i} className={styles.tag}>
                  {option}
                  <button type="button" className={styles.removeTag} onClick={() => handleRemoveOption(index, i)}>
                    ×
                  </button>
                </span>
              ))}
              <input
                type="text"
                placeholder={variant.options.length === 0 ? "Ange valmöjligheter som t.ex Blå, Svart, Grön" : ""}
                onKeyDown={(e) => {
                  if (e.key === "Enter" && e.currentTarget.value.trim()) {
                    handleAddOption(index, e.currentTarget.value.trim());
                    e.currentTarget.value = "";
                  }
                }}
                onBlur={(e) => {
                  if (e.currentTarget.value.trim()) {
                    handleAddOption(index, e.currentTarget.value.trim());
                    e.currentTarget.value = "";
                  }
                }}
              />
            </div>
          </div>
          {/* Remove Variant Button */}
          <button type="button" className={styles.removeVariantButton} onClick={() => handleRemoveVariant(index)}>
            ×
          </button>
        </div>
      ))}
      {/* Add New Variant Button */}
      <button type="button" className={styles.addVariantButton} onClick={handleAddVariant}>
        + Nytt produktval
      </button>

      {/* Variant Combinations Table */}
      {showTable && combinations.length > 0 && (
        <table className={styles.variantTable}>
          <thead>
            <tr>
              {variants
                .filter((variant) => variant.options.length > 0) // Only include variants with options.
                .map((variant, i) => (
                  <th key={i}>{variant.title || "N/A"}</th>
                ))}
              <th>Artikelnummer</th>
              <th>Pris</th>
            </tr>
          </thead>
          <tbody>
            {combinations.map((combination, i) => (
              <tr key={i}>
                {combination.map((value, j) => (
                  <td key={j}>{value}</td>
                ))}
                <td>
                  <input type="text" placeholder={`Artikelnummer (${combination.join("-")})`} />
                </td>
                <td>
                  <input type="text" placeholder="Pris" />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Variant;
